using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace FLib
{
    /// <summary>
    /// ２つの輪郭線の類似度を比較する。DPで各点の対応関係を求めて、Shape contextから類似度を求める。
    /// "Globally Optimal Toon Tracking" [H.Zhu et al, 2016] を参考に実装
    /// </summary>
    public class ShapeComparer
    {
        public bool DumpOnCalcDissimilarity { get; set; } // CalcDissimilarity()実行中にダンプ出力をするか

        // Shape contextの分解能
        public int AngleBinNum { get; private set; }
        public int DistBinNum { get; private set; }

        public ShapeComparer(int angleBinNum, int distBinNum)
        {
            AngleBinNum = angleBinNum;
            DistBinNum = distBinNum;
        }

        /// <summary>
        /// ２つの輪郭 path1, path2 の類似度を計算する。
        /// </summary>
        /// <returns>類似度 (値域は[0, 1]) </returns>
        public float CalcDissimilarity(List<PointF> path1, List<PointF> path2)
        {
            if (AngleBinNum * DistBinNum <= 0)
            {
                return 0.0f;
            }

            if (path1.Count <= 0 || path2.Count <= 0)
            {
                return 1.0f;
            }

            // path2が要素数が多くなるように適宜スワップ
            if (path1.Count > path2.Count)
            {
                var tmp = path2;
                path2 = path1;
                path1 = tmp;
            }

            // path1, path2の各点の対応関係を計算
            float cost;
            List<Tuple<int, int>> pairs = findCorrespondingPairs(path1, path2, out cost);

            // path1, path2の計上の類似度を計算する（shape contextを比較する）
            float totalValue = 0.0f;
            foreach (var tpl in pairs)
            {
                var hist1 = new ShapeHistgram(path1, path1[tpl.Item1], AngleBinNum, DistBinNum);
                var hist2 = new ShapeHistgram(path2, path2[tpl.Item2], AngleBinNum, DistBinNum);

                for (int i = 0; i < AngleBinNum * DistBinNum; i++)
                {
                    // Histgram[i]の値域 = [0, 1]
                    float delta = hist1.Histgram[i] - hist2.Histgram[i];
                    if (hist1.Histgram[i] + hist2.Histgram[i] >= 1e-4f)
                    {
                        totalValue += 0.5f * delta * delta / (hist1.Histgram[i] + hist2.Histgram[i]);
                    }
                }
            }

            // 類似度を計算 [0, 1]
            float dissimilarity = totalValue / pairs.Count;// / maxValue);
            return dissimilarity;
        }

        /// <summary>
        /// DPでpath1, path2の各点の対応関係を計算して、対応する点のリストを返す
        /// </summary>
        /// <returns>corresponding pairs {(p1, p2)}, where p1 in path1 and p2 in path2.</returns>
        List<Tuple<int, int>> findCorrespondingPairs(List<PointF> path1, List<PointF> path2, out float cost)
        {
            var pairs = new List<Tuple<int, int>>();
            cost = 0.0f;

            if (path1 == null || path2 == null)
            {
                return pairs;
            }

            int skipNum = path1.Count + 1;

            // for optimize: 範囲外インデックスを mod でなく配列参照で解決できるようにする
            int[] toPath1 = Enumerable.Range(0, skipNum).Select(i => i % path1.Count).ToArray();
            int[] toPath2 = Enumerable.Range(0, 2 * path2.Count).Select(i => i % path2.Count).ToArray();

            // path2を二倍に伸ばした列と、path1の最適なマッチングをとる（path2の同じ要素が２回登場してはいけない）
            float[] dp = new float[skipNum * path2.Count * 2]; // dp table
            int[] backtrace1 = new int[skipNum * path2.Count * 2]; // backtrace用のデータ
            int[] backtrace2 = new int[skipNum * path2.Count * 2]; // backtrace用のデータ
            int[] startPoint1 = new int[skipNum * path2.Count * 2]; // DP計算途中で使うデータ。線分の開始点
            int[] startPoint2 = new int[skipNum * path2.Count * 2]; // DP計算途中で使うデータ。線分の開始点

            // STEP1: DPの各配列の初期化
            dp[0] = 0.0f;
            backtrace1[0] = 0;
            backtrace2[0] = 0;
            for (int i1 = 1; i1 < skipNum; i1++)
            {
                dp[i1] = i1;
                backtrace1[i1] = -1;
                backtrace2[i1] = -1;
                startPoint1[i1] = i1;
                startPoint2[i1] = 0;
            }
            for (int i2 = 0; i2 < path2.Count; i2++)
            {
                dp[i2 * skipNum] = 0;
                backtrace1[i2 * skipNum] = -1;
                backtrace2[i2 * skipNum] = -1;
                startPoint1[i2 * skipNum] = 0;
                startPoint2[i2 * skipNum] = i2;
            }
            for (int i2 = path2.Count; i2 < 2 * path2.Count; i2++)
            {
                dp[i2 * skipNum] = float.MaxValue;
                backtrace1[i2 * skipNum] = -1;
                backtrace2[i2 * skipNum] = -1;
                startPoint1[i2 * skipNum] = -1;
                startPoint2[i2 * skipNum] = -1;
            }

            // STEP2: update DP table
            for (int i2 = 1; i2 < path2.Count * 2; i2++)
            {
                for (int i1 = 1; i1 < path1.Count + 1; i1++)
                {                    
                    int index = i1 + i2 * skipNum;

                    // ∞が設定されているセルはスキップ
                    if (dp[index] == float.MaxValue)
                    {
                        continue;
                    }

                    int indexV = i1 + (i2 - 1) * skipNum; // ひとつ上
                    int indexH = (i1 - 1) + i2 * skipNum; // ひとつ左
                    
                    float costV = dp[indexV] == float.MaxValue ? float.MaxValue : dp[indexV] + 1;
                    float costH = dp[indexH] == float.MaxValue ? float.MaxValue : dp[indexH] + 1;
                    float costD = float.MaxValue;

                    for (int k = Math.Max(0, i2 - path2.Count); k < i2; k++)
                    {
                        int indexD = (i1 - 1) + k * skipNum;

                        float val = dp[indexD];
                        if (val == float.MaxValue)
                        {
                            continue;
                        }

                        float diff = calcDiff(
                            path1[toPath1[startPoint1[indexD]]], path1[toPath1[i1]], 
                            path2[toPath2[startPoint2[indexD]]], path2[toPath2[i2]]);

                        if (costD >= diff + val)
                        {
                            costD = diff + val;
                            dp[index] = costD;
                            backtrace1[index] = i1 - 1;
                            backtrace2[index] = k;
                            startPoint1[index] = i1;
                            startPoint2[index] = i2;
                        }
                    }

                    if (costV < costH && costV < costD) // 上のセルからの伝搬コストが最小の場合
                    {
                        dp[index] = costV;
                        backtrace1[index] = backtrace1[indexV];
                        backtrace2[index] = backtrace2[indexV];
                        startPoint1[index] = startPoint1[indexV];
                        startPoint2[index] = startPoint2[indexV];
                    }
                    else if (costH < costV && costH < costD) // 左のセルからの伝搬コストが最小の場合
                    {
                        dp[index] = costH;
                        backtrace1[index] = backtrace1[indexH];
                        backtrace2[index] = backtrace2[indexH];
                        startPoint1[index] = startPoint1[indexH];
                        startPoint2[index] = startPoint2[indexH];
                    }
                }
            }

            // STEP3: backtraceして、path1, path2の対応関係pairsを求める
            float minVal = float.MaxValue;
            int minIndex2 = 0;
            for (int i2 = 0; i2 < path2.Count * 2; i2++)
            {
                int index = path1.Count + i2 * skipNum;
                if (minVal > dp[index])
                {
                    minVal = dp[index];
                    minIndex2 = i2;
                }
            }

            int idx1 = backtrace1[path1.Count + minIndex2 * skipNum];
            int idx2 = backtrace2[path1.Count + minIndex2 * skipNum];
            while (idx1 >= 0 && idx2 >= 0)
            {
                pairs.Add(new Tuple<int, int>(toPath1[idx1], toPath2[idx2]));

                int idx = idx1 + idx2 * skipNum;
                if (idx1 == backtrace1[idx] && idx2 == backtrace2[idx])
                {
                    break;
                }
                
                idx1 = backtrace1[idx];
                idx2 = backtrace2[idx];
            }

            pairs.Reverse();

            // 必要ならダンプ出力
            if (DumpOnCalcDissimilarity)
            { 
                string dumpText = DebugUtility.DumpArrayToCsv("path1(X)", path1.Select(p => p.X).ToArray());
                dumpText += DebugUtility.DumpArrayToCsv("path1(Y)", path1.Select(p => p.Y).ToArray());
                dumpText += DebugUtility.DumpArrayToCsv("path2(X)", path2.Select(p => p.X).ToArray());
                dumpText += DebugUtility.DumpArrayToCsv("path2(Y)", path2.Select(p => p.Y).ToArray());
                dumpText += "\n";
                dumpText += "\n";

                dumpText += DebugUtility.DumpMatrixToCsv("DP", dp, skipNum);
                dumpText += string.Format("\n");
                dumpText += string.Format("\n");

                dumpText += DebugUtility.DumpMatrixToCsv("LastPoint", 
                    Enumerable.Range(0, startPoint1.Length).Select(i => startPoint1[i] + "|" + startPoint2[i]).ToArray(),
                    skipNum);
                dumpText += string.Format("\n");
                dumpText += string.Format("\n");

                dumpText += DebugUtility.DumpMatrixToCsv("BackTrace",
                    Enumerable.Range(0, backtrace2.Length).Select(i => backtrace1[i] + "|" + backtrace2[i]).ToArray(),
                    skipNum);
                dumpText += string.Format("\n");
                dumpText += string.Format("\n");

                foreach (var p in pairs)
                {
                    dumpText += string.Format("{0},{1}\n", p.Item1, p.Item2);
                }

                DebugUtility.SaveAsCsv("ShapeComparer", dumpText, openAfterSave: true);
            }

            return pairs;
        }

        float calcDiff(
            PointF start1, PointF end1,
            PointF start2, PointF end2)
        {
            float vx1 = end1.X - start1.X;
            float vy1 = end1.Y - start1.Y;
            double distance1 = Math.Sqrt(vx1 * vx1 + vy1 * vy1);

            float vx2 = end2.X - start2.X;
            float vy2 = end2.Y - start2.Y;
            double distance2 = Math.Sqrt(vx2 * vx2 + vy2 * vy2);

            if (distance1 <= 1e-4f || distance2 <= 1e-4f)
            {
                return float.MaxValue;
            }

            float dx = vx1 - vx2;
            float dy = vy1 - vy2;
            float diff = (float)(Math.Sqrt(dx *dx + dy * dy) / (distance1 + distance2));

            return diff;
        }
    }

    class ShapeHistgram
    {
        public float[] Histgram { get; private set; }
        public int BinNum { get { return Histgram == null ? 0 : Histgram.Length; } }

        public ShapeHistgram()
        {

        }

        public ShapeHistgram(List<PointF> path, PointF origin, int angleBinNum, int distBinNum)
        {
            CalcAndSetShapeHistgram(path, origin, angleBinNum, distBinNum);
        }

        unsafe public void CalcAndSetShapeHistgram(List<PointF> path, PointF origin, int angleBinNum, int distBinNum)
        {
            // initialize
            bool result = createShapeHistgram(angleBinNum, distBinNum);
            if (false == result)
            {
                return;
            }

            double distRatio = distBinNum / 20.0f; // e^20が最大値
            double angleRatio = angleBinNum / Math.PI;

            // counting
            float cnt = 0;
            for (int i = 0; i < path.Count; i++)
            {
                double dx = path[i].X - origin.X;
                double dy = path[i].Y - origin.Y;

                double distance = Math.Sqrt(dx * dx + dy * dy);
                double logDistance = Math.Log(distance);
                int logDistanceBin = (int)(logDistance * distRatio);
                logDistanceBin = Math.Min(distBinNum, Math.Max(0, logDistanceBin));

                double angle = Math.Atan2(dy, dx);
                int angleBin = (int)((angle + Math.PI * 0.5f) * angleRatio);
                angleBin = Math.Min(angleBinNum, Math.Max(0, angleBin));

                Histgram[logDistanceBin * angleBinNum + angleBin]++;
                cnt++;
            }

            if (cnt >= 1)
            {
                for (int i = 0; i < Histgram.Length; i++)
                {
                    Histgram[i] /= cnt;
                }
            }
        }

        bool createShapeHistgram(int angleBinNum, int distBinNum)
        {
            int binNum = angleBinNum * distBinNum;
            System.Diagnostics.Debug.Assert(binNum >= 1);

            if (binNum <= 0)
            {
                return false;
            }

            Histgram = new float[binNum];
            for (int i = 0; i < Histgram.Length; i++)
            {
                Histgram[i] = 0;
            }

            return true;
        }
    }
}
