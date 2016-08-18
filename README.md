DemoShapeComperer
====================

**DemoShapeComperer** : A frontend software for testing shape comparison.

Introduction
--------------------
This software is a middle outcome of my project to reimplement "Globally Optimal Toon Tracking" [Haichao Zhu et al., SIGGRAPH 2016].

This software calculates a similarity of two user-input strokes.

**Example: start vs star**
![Imgur](http://i.imgur.com/H2kK6s9.png)

**Example: star vs circle**
![Imgur](http://i.imgur.com/R7ejfBl.png)


How to use
--------------------
Draw two strokes on the two pictureboxes on the window and press "Calculate" button.

This software calculates the dissimilarity (= 1.0 - similarity)  and show the value on the window.

if the value is low, the two stroke are similar, and vice versa.

Implementation and Requirements
--------------------

This software is implemented as a WinForm application with Visual C# 2013.
I confirmed this software works on both Windows 10 in ThinkPad X1 Carbon.

Licence
--------------------

The MIT License (MIT)

Copyright (c) 2016 furaga

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.