using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MayTheFourth.States {
    public enum GameState {
        None,

        // Popup states
        Paused,

        // Exclusive states
        Title,
        Playing,
        GameOver,
    }
}
