using System;

namespace Contracts {

    public static class ContractHelper {
    

        public static Placement DeserializePlacement(byte[] contract, ref uint offset) {
            Placement placement = new Placement();
            byte numMarbles = contract[offset];
            offset++;
            for (uint i = 0; i < numMarbles; i++) {
                Marbles.Type type = (Marbles.Type) contract[offset];
                offset++;
                Marbles.Color color = (Marbles.Color) contract[offset];
                offset++;

                byte[] quantityBytes = new byte[4];
                Array.Copy(contract, offset, quantityBytes, 0, 4);
                uint quantity = BitConverter.ToUInt32(quantityBytes, 0);

                placement.Add(type, color, quantity);
                offset += 4;
            }

            return placement;
        }

        public static Placement DeserializePlacement(byte[] contract) {
            uint k = 0;
            return DeserializePlacement(contract, ref k);
        }

        private static byte[] DeserializeAddress(byte[] data, ref uint offset) {
            byte[] pubKeyHash = new byte[25];
            Array.Copy(data, offset, pubKeyHash, 0, 25);
            
            offset += 25;
            return pubKeyHash;
        }
    }
}
