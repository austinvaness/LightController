// <auto-generated>
//   This file was generated by a tool; you should avoid making direct changes.
//   Consider using 'partial classes' to extend these types
//   Input: hotKey.proto
// </auto-generated>

#region Designer generated code

namespace rv.data
{

    [global::ProtoBuf.ProtoContract()]
    public partial class HotKey : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, Name = @"code")]
        public KeyCode Code { get; set; }

        [global::ProtoBuf.ProtoMember(2, Name = @"control_identifier")]
        [global::System.ComponentModel.DefaultValue("")]
        public string ControlIdentifier { get; set; } = "";

        [global::ProtoBuf.ProtoContract()]
        public enum KeyCode
        {
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_UNKNOWN")]
            KeyCodeUnknown = 0,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_A")]
            KeyCodeAnsiA = 1,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_B")]
            KeyCodeAnsiB = 2,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_C")]
            KeyCodeAnsiC = 3,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_D")]
            KeyCodeAnsiD = 4,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_E")]
            KeyCodeAnsiE = 5,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_F")]
            KeyCodeAnsiF = 6,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_G")]
            KeyCodeAnsiG = 7,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_H")]
            KeyCodeAnsiH = 8,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_I")]
            KeyCodeAnsiI = 9,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_J")]
            KeyCodeAnsiJ = 10,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_K")]
            KeyCodeAnsiK = 11,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_L")]
            KeyCodeAnsiL = 12,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_M")]
            KeyCodeAnsiM = 13,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_N")]
            KeyCodeAnsiN = 14,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_O")]
            KeyCodeAnsiO = 15,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_P")]
            KeyCodeAnsiP = 16,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_Q")]
            KeyCodeAnsiQ = 17,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_R")]
            KeyCodeAnsiR = 18,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_S")]
            KeyCodeAnsiS = 19,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_T")]
            KeyCodeAnsiT = 20,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_U")]
            KeyCodeAnsiU = 21,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_V")]
            KeyCodeAnsiV = 22,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_W")]
            KeyCodeAnsiW = 23,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_X")]
            KeyCodeAnsiX = 24,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_Y")]
            KeyCodeAnsiY = 25,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_Z")]
            KeyCodeAnsiZ = 26,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_0")]
            KeyCodeAnsi0 = 27,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_1")]
            KeyCodeAnsi1 = 28,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_2")]
            KeyCodeAnsi2 = 29,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_3")]
            KeyCodeAnsi3 = 30,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_4")]
            KeyCodeAnsi4 = 31,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_5")]
            KeyCodeAnsi5 = 32,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_6")]
            KeyCodeAnsi6 = 33,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_7")]
            KeyCodeAnsi7 = 34,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_8")]
            KeyCodeAnsi8 = 35,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_9")]
            KeyCodeAnsi9 = 36,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_EQUAL")]
            KeyCodeAnsiEqual = 37,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_MINUS")]
            KeyCodeAnsiMinus = 38,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_RIGHT_BRACKET")]
            KeyCodeAnsiRightBracket = 39,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_LEFT_BRACKET")]
            KeyCodeAnsiLeftBracket = 40,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_QUOTE")]
            KeyCodeAnsiQuote = 41,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_SEMICOLON")]
            KeyCodeAnsiSemicolon = 42,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_BACKSLASH")]
            KeyCodeAnsiBackslash = 43,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_COMMA")]
            KeyCodeAnsiComma = 44,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_SLASH")]
            KeyCodeAnsiSlash = 45,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_PERIOD")]
            KeyCodeAnsiPeriod = 46,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_GRAVE")]
            KeyCodeAnsiGrave = 47,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_KEYPAD_DECIMAL")]
            KeyCodeAnsiKeypadDecimal = 48,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_KEYPAD_PLUS")]
            KeyCodeAnsiKeypadPlus = 49,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_KEYPAD_CLEAR")]
            KeyCodeAnsiKeypadClear = 50,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_KEYPAD_DIVIDE")]
            KeyCodeAnsiKeypadDivide = 51,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_KEYPAD_ENTER")]
            KeyCodeAnsiKeypadEnter = 52,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_KEYPAD_MINUS")]
            KeyCodeAnsiKeypadMinus = 53,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_KEYPAD_EQUALS")]
            KeyCodeAnsiKeypadEquals = 54,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_KEYPAD_0")]
            KeyCodeAnsiKeypad0 = 55,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_KEYPAD_1")]
            KeyCodeAnsiKeypad1 = 56,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_KEYPAD_2")]
            KeyCodeAnsiKeypad2 = 57,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_KEYPAD_3")]
            KeyCodeAnsiKeypad3 = 58,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_KEYPAD_4")]
            KeyCodeAnsiKeypad4 = 59,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_KEYPAD_5")]
            KeyCodeAnsiKeypad5 = 60,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_KEYPAD_6")]
            KeyCodeAnsiKeypad6 = 61,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_KEYPAD_7")]
            KeyCodeAnsiKeypad7 = 62,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_KEYPAD_8")]
            KeyCodeAnsiKeypad8 = 63,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ANSI_KEYPAD_9")]
            KeyCodeAnsiKeypad9 = 64,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_F1")]
            KeyCodeF1 = 65,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_F2")]
            KeyCodeF2 = 66,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_F3")]
            KeyCodeF3 = 67,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_F4")]
            KeyCodeF4 = 68,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_F5")]
            KeyCodeF5 = 69,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_F6")]
            KeyCodeF6 = 70,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_F7")]
            KeyCodeF7 = 71,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_F8")]
            KeyCodeF8 = 72,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_F9")]
            KeyCodeF9 = 73,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_F10")]
            KeyCodeF10 = 74,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_F11")]
            KeyCodeF11 = 75,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_F12")]
            KeyCodeF12 = 76,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_F13")]
            KeyCodeF13 = 77,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_F14")]
            KeyCodeF14 = 78,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_F15")]
            KeyCodeF15 = 79,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_F16")]
            KeyCodeF16 = 80,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_F17")]
            KeyCodeF17 = 81,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_F18")]
            KeyCodeF18 = 82,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_F19")]
            KeyCodeF19 = 83,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_F20")]
            KeyCodeF20 = 84,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_FUNCTION")]
            KeyCodeFunction = 85,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_RETURN")]
            KeyCodeReturn = 86,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_TAB")]
            KeyCodeTab = 87,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_SPACE")]
            KeyCodeSpace = 88,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_DELETE")]
            KeyCodeDelete = 89,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ESCAPE")]
            KeyCodeEscape = 90,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_COMMAND")]
            KeyCodeCommand = 91,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_SHIFT")]
            KeyCodeShift = 92,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_CAPS_LOCK")]
            KeyCodeCapsLock = 93,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_OPTION")]
            KeyCodeOption = 94,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_CONTROL")]
            KeyCodeControl = 95,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_RIGHT_SHIFT")]
            KeyCodeRightShift = 96,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_RIGHT_OPTION")]
            KeyCodeRightOption = 97,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_RIGHT_CONTROL")]
            KeyCodeRightControl = 98,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_VOLUME_UP")]
            KeyCodeVolumeUp = 99,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_VOLUME_DOWN")]
            KeyCodeVolumeDown = 100,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_MUTE")]
            KeyCodeMute = 101,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_HELP")]
            KeyCodeHelp = 102,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_HOME")]
            KeyCodeHome = 103,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_PAGE_UP")]
            KeyCodePageUp = 104,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_FORWARD_DELETE")]
            KeyCodeForwardDelete = 105,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_END")]
            KeyCodeEnd = 106,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_PAGE_DOWN")]
            KeyCodePageDown = 107,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_LEFT_ARROW")]
            KeyCodeLeftArrow = 108,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_RIGHT_ARROW")]
            KeyCodeRightArrow = 109,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_DOWN_ARROW")]
            KeyCodeDownArrow = 110,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_UP_ARROW")]
            KeyCodeUpArrow = 111,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_ISO_SELECTION")]
            KeyCodeIsoSelection = 112,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_JIS_YEN")]
            KeyCodeJisYen = 113,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_JIS_UNDERSCORE")]
            KeyCodeJisUnderscore = 114,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_JIS_KEYPAD_COMMA")]
            KeyCodeJisKeypadComma = 115,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_JIS_EISU")]
            KeyCodeJisEisu = 116,
            [global::ProtoBuf.ProtoEnum(Name = @"KEY_CODE_JIS_KANA")]
            KeyCodeJisKana = 117,
        }

    }

}


#endregion
