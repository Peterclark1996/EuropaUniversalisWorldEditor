using System;
using System.Windows;

namespace EuropaUniversalisWorldEditor
{
    public class Selection
    {
        private readonly UIElement _panel;
        private readonly Mod _currentMod;

        private Pixel _currentSelection;
        
        public Selection(UIElement panel, Mod currentMod)
        {
            _panel = panel;
            _currentMod = currentMod;
            
            _currentSelection = new Pixel(0, 0, 0);
        }

        public void SelectPixel(int x, int y)
        {
            _currentSelection = _currentMod.World.ProvinceMap.GetPixelAt(x, y);
            Console.WriteLine(_currentSelection.R + ", " + _currentSelection.G + ", " + _currentSelection.B);
        }

        public Pixel GetSelectedPixel()
        {
            return _currentSelection;
        }
    }
}