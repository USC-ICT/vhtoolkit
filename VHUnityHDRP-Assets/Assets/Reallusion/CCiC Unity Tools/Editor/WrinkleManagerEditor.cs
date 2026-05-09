/*
 *  This file is distributed as part of CC_Unity_Tools <https://github.com/soupday/CC_Unity_Tools>
 * 
 *  MIT No Attribution (https://github.com/aws/mit-0)
 *
 *  Copyright 2025 Victor Soupday
 *
 *  Permission is hereby granted, free of charge, to any person obtaining a copy of this
 *  software and associated documentation files (the "Software"), to deal in the Software
 *  without restriction, including without limitation the rights to use, copy, modify,
 *  merge, publish, distribute, sublicense, and/or sell copies of the Software, and to
 *  permit persons to whom the Software is furnished to do so.
 *
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
 *  INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A
 *  PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
 *  HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
 *  OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
 *  SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

using UnityEngine;
using UnityEditor;

namespace Reallusion.Runtime
{
    [CustomEditor(typeof(WrinkleManager))]
    public class WrinkleManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI(); // Draw the default inspector

            WrinkleManager wrinkleManager = (WrinkleManager)target;

            if (GUILayout.Button("Update BlendShape Indices"))
            {
                wrinkleManager.UpdateBlendShapeIndices(); // Assuming this is a public method
            }
        }
    }
}