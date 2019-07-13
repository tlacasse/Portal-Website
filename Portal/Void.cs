using System;

namespace Portal {

    public sealed class Void {

        private Void() {
            throw new InvalidOperationException();
        }

    }

}
