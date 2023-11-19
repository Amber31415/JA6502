using System;

namespace emu {
  enum OpCode : byte {
    LDA_IM = 0xA9,
    LDA_ZP = 0xA5,
    LDA_ZX = 0xB5,
    LDA_AB = 0xAD,
    LDA_AX = 0xBD,
    LDA_AY = 0xB9,
    LDA_IX = 0xA1,
    LDA_IY = 0xB1
  }

  class CPU {
    public ushort PC;
    private byte SP;
    private byte A, X, Y;

    private bool C, Z, I, D, B, O, N;

    // Debugging purposes only
    public ushort clock = 0;

    public byte[] memory = new byte[0xFFFF];

    // Address register
    private byte ADL, ADH;

    // Address bus value
//    private ushort address;

    // Data bus value
//    private byte data;


    public byte ReadByte(ushort address) {
      clock++;
      return memory[address];
    }

    public byte ReadByte(byte high, byte low) {
      clock++;
      return memory[(high << 8) | low];
    }

    public void cycle() {
      OpCode instuction = (OpCode)ReadByte(PC++);
      switch (instuction) {
        case OpCode.LDA_IM: {
          A = ReadByte(PC++);
          Z = A == 0;
          N = A >> 7 == 1;
          break;
        }
        case OpCode.LDA_ZP: {
          ADL = ReadByte(PC++);
          A = ReadByte(0x00, ADL);
          Z = A == 0;
          N = A >> 7 == 1;
          break;
        }
        case OpCode.LDA_ZX: {
          break;
        }
        case OpCode.LDA_AB: {
          ADL = ReadByte(PC++);
          ADH = ReadByte(PC++);
          A = ReadByte(ADH, ADL);
          Z = A == 0;
          N = A >> 7 == 1;
          break;
        }
        case OpCode.LDA_AX: {
          break;
        }
        case OpCode.LDA_AY: {
          break;
        }
        case OpCode.LDA_IX: {
          break;
        }
        case OpCode.LDA_IY: {
          break;
        }
      }
    }
  }

  class Program {
    static void Main(string[] args) {
      CPU cpu = new CPU();
      cpu.memory[0x02_42] = (byte)OpCode.LDA_AB;
      cpu.memory[0x02_43] = 0x34;
      cpu.memory[0x02_44] = 0x12;
      cpu.memory[0x12_34] = 0x57;
      cpu.PC = 0x02_42;
      cpu.cycle();
      cpu.cycle();
    }
  }
}