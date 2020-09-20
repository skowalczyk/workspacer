using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace workspacer
{
    public class TallLayoutEngine : ILayoutEngine
    {
        private readonly int _numInPrimary;
        private readonly double _primaryPercent;
        private readonly double _primaryPercentIncrement;

        private int _numInPrimaryOffset = 0;
        private double _primaryPercentOffset = 0;
        private bool _primaryFlip = false;

        public TallLayoutEngine() : this(1, 0.5, 0.03) { }

        public TallLayoutEngine(int numInPrimary, double primaryPercent, double primaryPercentIncrement)
        {
            _numInPrimary = numInPrimary;
            _primaryPercent = primaryPercent;
            _primaryPercentIncrement = primaryPercentIncrement;
        }

        public string Name => "tall";

        public IEnumerable<IWindowLocation> CalcLayout(IEnumerable<IWindow> windows, int spaceWidth, int spaceHeight)
        {
            var list = new List<IWindowLocation>();
            var numWindows = windows.Count();

            if (numWindows == 0)
                return list;

            int numInPrimary = Math.Min(GetNumInPrimary(), numWindows);

            int primaryWidth = (int)(spaceWidth * (_primaryPercent + _primaryPercentOffset));
            int primaryHeight = spaceHeight / numInPrimary;
            int height = spaceHeight / Math.Max(numWindows - numInPrimary, 1);

            // if there are more "primary" windows than actual windows,
            // then we want the pane to actually spread the entire width
            // of the working area
            if (numInPrimary >= numWindows)
            {
                primaryWidth = spaceWidth;
            }

            int secondaryWidth = spaceWidth - primaryWidth;

            for (var i = 0; i < numWindows; i++)
            {
                int windowXPosition, windowYPosition;
                int windowWidth, windowHeight;

                if (i < numInPrimary)
                {
                    windowXPosition = 0;
                    windowWidth = primaryWidth;
                    windowHeight = primaryHeight;
                    windowYPosition = i * windowHeight;
                    if (_primaryFlip && numInPrimary < numWindows)
                    {
                        windowXPosition = primaryWidth;
                        windowWidth = secondaryWidth;
                    }
                }
                else
                {
                    windowXPosition = primaryWidth;
                    windowWidth = secondaryWidth;
                    windowHeight = height;
                    windowYPosition = (i - numInPrimary) * windowHeight;
                    if (_primaryFlip)
                    {
                        windowXPosition = 0;
                        windowWidth = primaryWidth;
                    }
                }

                list.Add(new WindowLocation(windowXPosition, windowYPosition, windowWidth, windowHeight, WindowState.Normal));
            }
            return list;
        }
        public void FlipPrimaryArea()
        {
            _primaryFlip = !_primaryFlip;
            _primaryPercentOffset = -_primaryPercentOffset;
        }

        public void ShrinkPrimaryArea()
        {
            _primaryPercentOffset -= _primaryPercentIncrement;
        }

        public void ExpandPrimaryArea()
        {
            _primaryPercentOffset += _primaryPercentIncrement;
        }

        public void ResetPrimaryArea()
        {
            _primaryPercentOffset = 0;
        }

        public void IncrementNumInPrimary()
        {
            _numInPrimaryOffset++;
        }

        public void DecrementNumInPrimary()
        {
            if (GetNumInPrimary() > 1)
            {
                _numInPrimaryOffset--;
            }
        }

        private int GetNumInPrimary()
        {
            return _numInPrimary + _numInPrimaryOffset;
        }
    }
}
