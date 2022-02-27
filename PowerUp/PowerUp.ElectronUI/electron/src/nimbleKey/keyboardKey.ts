/** KeyboardEvent key Value Type
 * docs are copied from: https://www.w3.org/TR/uievents-key/#key-attribute-value
 */
export type KeyboardKey = PrintableKey | NamedKey | Unidentified;
export type KeyboardKeyType = 
| 'UnknownPrintable'
| 'UnknownNamed'
| typeof unidentified
| typeof alphabeticKeyType
| typeof numericKeyType
| typeof symbolKeyType
| typeof modifierKeyType
| typeof legacyModifierType
| typeof whitespaceKeyType
| typeof navigationKeyType
| typeof editingKeyType
| typeof uiKeyType
| typeof deviceKeyType
| typeof imeAndCompositionKeyType
| typeof functionKeyType
| typeof multimediaKeyType
| typeof multimediaNumpadKeyType
| typeof audioKeyType
| typeof speechKeyType
| typeof applicationKeyType
| typeof browserKeyType
| typeof mobilePhoneKeyType
| typeof tvKeyType
| typeof mediaControllerKeyType

export function getKeyType(key: string): KeyboardKeyType {
  if(isPrintableKey(key)){
    if(isAlphabeticKey(key)) return 'US-en Alphabetic';
    if(isNumericKey(key)) return 'Numeric';
    if(isSymbolKey(key)) return 'Symbol';
    else return 'UnknownPrintable';
  } 
  else if(isNamedKey(key)) {
    if(isModifierKey(key)) return 'Modifier';
    if(isLegacyModifierKey(key)) return 'LegacyModifier';
    if(isWhitespaceKey(key)) return 'Whitespace';
    if(isNavigationKey(key)) return 'Navigation';
    if(isEditingKey(key)) return 'Editing';
    if(isUIKey(key)) return 'UI';
    if(isDeviceKey(key)) return 'Device';
    if(isIMECompositionKey(key)) return 'IME/Composition';
    if(isFunctionKey(key)) return 'Function';
    if(isMultimediaKey(key)) return 'Multimedia';
    if(isMultimediaNumpadKey(key)) return 'MultimediaNumpadKey';
    if(isAudioKey(key)) return 'Audio';
    if(isSpeechKey(key)) return 'Speech';
    if(isApplicationKey(key)) return 'Application';
    if(isBrowserKey(key)) return 'Browser';
    if(isMobilePhoneKey(key)) return 'MobilePhone';
    if(isTVKey(key)) return 'TV';
    if(isMediaControllerKey(key)) return 'MediaController';
    else return 'UnknownNamed';
  } 
  else if(isUnidentified(key)) {
    return 'Unidentified';
  } 
  else {
    throw `Invalid key ${key}`; 
  }
}

export function keyEquals(key1: KeyboardKey, key2: KeyboardKey): boolean {
  return isPrintableKey(key1) && isPrintableKey(key2)
    ? key1.toLowerCase() === key2.toLowerCase()
    : key1 === key2;
}

export type PrintableKey = (string & {}) | US_En_AlphanumericKey | SymbolKey;
export function isPrintableKey(key: string): key is PrintableKey { 
  return key?.length === 1; 
}

export type NamedKey = (string & {}) | KnownNamedKey;
export function isNamedKey(key: string): key is NamedKey { return key?.length > 1 && key != unidentified; }

export type KnownNamedKey = 
| ModifierKey
| LegacyModifierKey
| WhitespaceKey
| NavigationKey
| EditingKey
| UIKey
| DeviceKey
| IMECompositionKey
| FunctionKey
| MultimediaKey
| MultimediaNumpadKey
| AudioKey
| SpeechKey
| ApplicationKey
| BrowserKey
| MobilePhoneKey
| TVKey
| MediaControllerKey
export function isKnownNamedKey(key: string): key is KnownNamedKey { 
  return isModifierKey(key) 
    || isLegacyModifierKey(key)
    || isWhitespaceKey(key)
    || isNavigationKey(key)
    || isEditingKey(key)
    || isUIKey(key)
    || isDeviceKey(key)
    || isIMECompositionKey(key)
    || isFunctionKey(key)
    || isMultimediaKey(key)
    || isMultimediaNumpadKey(key)
    || isAudioKey(key)
    || isSpeechKey(key)
    || isApplicationKey(key)
    || isBrowserKey(key)
    || isMobilePhoneKey(key)
    || isTVKey(key)
    || isMediaControllerKey(key); 
}

export type US_En_AlphanumericKey = US_En_AlphabeticKey | NumericKey;
export function isAlphanumericKey(key: string): key is US_En_AlphanumericKey { return isAlphabeticKey(key) || isNumericKey(key); }

export type US_En_AlphabeticKey = typeof alphabeticKeys[number];
export function isAlphabeticKey(key: string): key is US_En_AlphabeticKey { return alphabeticKeys.some(k => k === key); }
const alphabeticKeyType = 'US-en Alphabetic';
const alphabeticKeys = [
  'A',
  'B',
  'C',
  'D',
  'E',
  'F',
  'G',
  'H',
  'I',
  'J',
  'K',
  'L',
  'M',
  'N',
  'O',
  'P',
  'Q',
  'R',
  'S',
  'T',
  'U',
  'V',
  'W',
  'X',
  'Y',
  'Z',
  'a',
  'b',
  'c',
  'd',
  'e',
  'f',
  'g',
  'h',
  'i',
  'j',
  'k',
  'l',
  'j',
  'm',
  'n',
  'o',
  'p',
  'q',
  'r',
  's',
  't',
  'u',
  'v',
  'w',
  'x',
  'y',
  'z',
] as const

export type NumericKey = typeof numericKeys[number]
export function isNumericKey(key: string): key is NumericKey { return numericKeys.some(k => k === key); }
const numericKeyType = 'Numeric';
const numericKeys = [
  '1',
  '2',
  '3',
  '4',
  '5',
  '6',
  '7',
  '8',
  '9',
  '0',
] as const

export type SymbolKey = typeof symbolKeys[number]
export function isSymbolKey(key: string): key is NumericKey { return symbolKeys.some(k => k === key); }
const symbolKeyType = "Symbol";
const symbolKeys = [
  '`',
  '~',
  '!',
  '@',
  '#',
  '$',
  '%',
  '^',
  '&',
  '*',
  '(',
  ')',
  '-',
  '_',
  '=',
  '+',
  '[',
  '{',
  ']',
  '}',
  '\\',
  '|',
  ';',
  ':',
  '\'',
  '"',
  ',',
  '<',
  '.',
  '>',
  '/',
  '?',
  ' ',
] as const

export type Unidentified = typeof unidentified;
export function isUnidentified(key: string): key is Unidentified { return key === unidentified; }
const unidentified = 'Unidentified' //	This key value is used when an implementation is unable to identify another key value, due to either hardware, platform, or software constraints.

export type ModifierKey = typeof modifierKeys[number];
export function isModifierKey(key: string): key is ModifierKey { return modifierKeys.some(k => k === key ); }
const modifierKeyType = 'Modifier';
const modifierKeys = [
  'Alt', // The Alt (Alternative) key.This key enables the alternate modifier function for interpreting concurrent or subsequent keyboard input.This key value is also used for the Apple Option key.
  'AltGraph', // The Alternate Graphics (AltGr or AltGraph) key. This key is used enable the ISO Level 3 shift modifier (the standard Shift key is the level 2 modifier). See [ISO9995-1].
  'CapsLock', // The Caps Lock (Capital) key. Toggle capital character lock function for interpreting subsequent keyboard input event.
  'Control', // The Control or Ctrl key, to enable control modifier function for interpreting concurrent or subsequent keyboard input.
  'Fn', // The Function switch Fn key.Activating this key simultaneously with another key changes that key’s value to an alternate character or function. This key is often handled directly in the keyboard hardware and does not usually generate key events.
  'FnLock', // The Function-Lock (FnLock or F-Lock) key. Activating this key switches the mode of the keyboard to changes some keys' values to an alternate character or function. This key is often handled directly in the keyboard hardware and does not usually generate key events.
  'Meta', // The Meta key, to enable meta modifier function for interpreting concurrent or subsequent keyboard input. This key value is used for the Windows Logo key and the Apple Command or ⌘ key.
  'NumLock', // The NumLock or Number Lock key, to toggle numpad mode function for interpreting subsequent keyboard input.
  'ScrollLock', // The Scroll Lock key, to toggle between scrolling and cursor movement modes.
  'Shift', // The Shift key, to enable shift modifier function for interpreting concurrent or subsequent keyboard input.
  'Symbol', // The Symbol modifier key (used on some virtual keyboards).
  'SymbolLock', // The Symbol Lock key.
] as const

export type LegacyModifierKey = typeof legacyModifierKeys[number];
export function isLegacyModifierKey(key: string): key is LegacyModifierKey { return legacyModifierKeys.some(k => k === key); }
const legacyModifierType = 'LegacyModifier';
const legacyModifierKeys = [
  'Hyper', // The Hyper key.
  'Super', // The Super key.
] as const

export type WhitespaceKey = typeof whitespaceKeys[number];
export function isWhitespaceKey(key: string): key is WhitespaceKey { return whitespaceKeys.some(k => k === key); }
const whitespaceKeyType = 'Whitespace';
const whitespaceKeys = [
  'Enter', // The Enter or ↵ key, to activate current selection or accept current input.This key value is also used for the Return (Macintosh numpad) key.This key value is also used for the Android KEYCODE_DPAD_CENTER.
  'Tab', // The Horizontal Tabulation Tab key.
] as const

export type NavigationKey = typeof navigationKeys[number];
export function isNavigationKey(key: string): key is NavigationKey { return navigationKeys.some(k => k === key); }
const navigationKeyType = 'Navigation';
const navigationKeys = [
  'ArrowDown', // The down arrow key, to navigate or traverse downward. (KEYCODE_DPAD_DOWN)
  'ArrowLeft', // The left arrow key, to navigate or traverse leftward. (KEYCODE_DPAD_LEFT)
  'ArrowRight', // The right arrow key, to navigate or traverse rightward. (KEYCODE_DPAD_RIGHT)
  'ArrowUp', // The up arrow key, to navigate or traverse upward. (KEYCODE_DPAD_UP)
  'End', // The End key, used with keyboard entry to go to the end of content (KEYCODE_MOVE_END).
  'Home', // The Home key, used with keyboard entry, to go to start of content (KEYCODE_MOVE_HOME).For the mobile phone Home key (which goes to the phone’s main screen), use "GoHome".
  'PageDown', // The Page Down key, to scroll down or display next page of content.
  'PageUp', // The Page Up key, to scroll up or display previous page of content.
] as const

export type EditingKey = typeof editingKeys[number];
export function isEditingKey(key: string): key is EditingKey { return editingKeys.some(k => k === key); }
const editingKeyType = 'Editing';
const editingKeys = [
  'Backspace', // The Backspace key. This key value is also used for the key labeled Delete on MacOS keyboards.
  'Clear', // Remove the currently selected input.
  'Copy', // Copy the current selection. (APPCOMMAND_COPY)
  'CrSel', // The Cursor Select (Crsel) key.
  'Cut', // Cut the current selection. (APPCOMMAND_CUT)
  'Delete', // The Delete (Del) Key. This key value is also used for the key labeled Delete on MacOS keyboards when modified by the Fn key.
  'EraseEof', // The Erase to End of Field key. This key deletes all characters from the current cursor position to the end of the current field.
  'ExSel', // The Extend Selection (Exsel) key.
  'Insert', // The Insert (Ins) key, to toggle between text modes for insertion or overtyping. (KEYCODE_INSERT)
  'Paste', // The Paste key. (APPCOMMAND_PASTE)
  'Redo', // Redo the last action. (APPCOMMAND_REDO)
  'Undo', // Undo the last action. (APPCOMMAND_UNDO)
] as const

export type UIKey = typeof uiKeys[number];
export function isUIKey(key: string): key is UIKey { return uiKeys.some(k => k === key); }
const uiKeyType = 'UI';
const uiKeys = [
  'Accept', // The Accept (Commit, OK) key. Accept current option or input method sequence conversion.
  'Again', // The Again key, to redo or repeat an action.
  'Attn', // The Attention (Attn) key.
  'Cancel', // The Cancel key.
  'ContextMenu', // Show the application’s context menu. This key is commonly found between the right Meta key and the right Control key.
  'Escape', // The Esc key. This key was originally used to initiate an escape sequence, but is now more generally used to exit or "escape" the current context, such as closing a dialog or exiting full screen mode.
  'Execute', // The Execute key.
  'Find', // Open the Find dialog. (APPCOMMAND_FIND)
  'Help', // Open a help dialog or toggle display of help information. (APPCOMMAND_HELP, KEYCODE_HELP)
  'Pause', // Pause the current state or application (as appropriate).Do not use this value for the Pause button on media controllers. Use "MediaPause" instead.
  'Play', // Play or resume the current state or application (as appropriate).Do not use this value for the Play button on media controllers. Use "MediaPlay" instead.
  'Props', // The properties (Props) key.
  'Select', // The Select key.
  'ZoomIn', // The ZoomIn key. (KEYCODE_ZOOM_IN)
  'ZoomOut', // The ZoomOut key. (KEYCODE_ZOOM_OUT)
] as const

export type DeviceKey = typeof deviceKeys[number];
export function isDeviceKey(key: string): key is DeviceKey { return deviceKeys.some(k => k === key); }
const deviceKeyType = 'Device';
const deviceKeys = [
  'BrightnessDown', // The Brightness Down key. Typically controls the display brightness. (KEYCODE_BRIGHTNESS_DOWN)
  'BrightnessUp', // The Brightness Up key. Typically controls the display brightness. (KEYCODE_BRIGHTNESS_UP)
  'Eject', // Toggle removable media to eject (open) and insert (close) state. (KEYCODE_MEDIA_EJECT)
  'LogOff', // The LogOff key.
  'Power', // Toggle power state. (KEYCODE_POWER)Note: Some devices might not expose this key to the operating environment.
  'PowerOff', // The PowerOff key. Sometime called PowerDown.
  'PrintScreen', // The Print Screen or SnapShot key, to initiate print-screen function.
  'Hibernate', // The Hibernate key. This key saves the current state of the computer to disk so that it can be restored. The computer will then shutdown.
  'Standby', // The Standby key. This key turns off the display and places the computer into a low-power mode without completely shutting down. It is sometimes labelled Suspend or Sleep key. (KEYCODE_SLEEP)
  'WakeUp', // The WakeUp key. (KEYCODE_WAKEUP)
] as const

export type IMECompositionKey = typeof imeAndCompositionKeys[number];
export function isIMECompositionKey(key: string): key is IMECompositionKey { return imeAndCompositionKeys.some(k => k === key); }
const imeAndCompositionKeyType = 'IME/Composition';
const imeAndCompositionKeys = [
  'AllCandidates', // The All Candidates key, to initate the multi-candidate mode.
  'Alphanumeric', // The Alphanumeric key.
  'CodeInput', // The Code Input key, to initiate the Code Input mode to allow characters to be entered by their code points.
  'Compose', // The Compose key, also known as Multi_key on the X Window System. This key acts in a manner similar to a dead key, triggering a mode where subsequent key presses are combined to produce a different character.
  'Convert', // The Convert key, to convert the current input method sequence.
  'Dead', // A dead key combining key. It may be any combining key from any keyboard layout. For example, on a PC/AT French keyboard, using a French mapping and without any modifier activiated, this is the key value U+0302 COMBINING CIRCUMFLEX ACCENT. In another layout this might be a different unicode combining key.For applications that need to differentiate between specific combining characters, the associated compositionupdate event’s data attribute provides the specific key value.
  'FinalMode', // The Final Mode Final key used on some Asian keyboards, to enable the final mode for IMEs.
  'GroupFirst', // Switch to the first character group. (ISO/IEC 9995)
  'GroupLast', // Switch to the last character group. (ISO/IEC 9995)
  'GroupNext', // Switch to the next character group. (ISO/IEC 9995)
  'GroupPrevious', // Switch to the previous character group. (ISO/IEC 9995)
  'ModeChange', // The Mode Change key, to toggle between or cycle through input modes of IMEs.
  'NextCandidate', // The Next Candidate function key.
  'NonConvert', // The NonConvert ("Don’t Convert") key, to accept current input method sequence without conversion in IMEs.
  'PreviousCandidate', // The Previous Candidate function key.
  'Process', // The Process key.
  'SingleCandidate', // The Single Candidate function key.
  // Korean
  'HangulMode', // The Hangul (Korean characters) Mode key, to toggle between Hangul and English modes.
  'HanjaMode', // The Hanja (Korean characters) Mode key.
  'JunjaMode', // The Junja (Korean characters) Mode key.
  // Japanese
  'Eisu', // The Eisu key. This key may close the IME, but its purpose is defined by the current IME. (KEYCODE_EISU)
  'Hankaku', // The (Half-Width) Characters key.
  'Hiragana', // The Hiragana (Japanese Kana characters) key.
  'HiraganaKatakana', // The Hiragana/Katakana toggle key. (KEYCODE_KATAKANA_HIRAGANA)
  'KanaMode', // The Kana Mode (Kana Lock) key. This key is used to enter hiragana mode (typically from romaji mode).
  'KanjiMode', // The Kanji (Japanese name for ideographic characters of Chinese origin) Mode key. This key is typically used to switch to a hiragana keyboard for the purpose of converting input into kanji. (KEYCODE_KANA)
  'Katakana', // The Katakana (Japanese Kana characters) key.
  'Romaji', // The Roman characters function key.
  'Zenkaku', // The Zenkaku (Full-Width) Characters key.
  'ZenkakuHankaku', // The Zenkaku/Hankaku (full-width/half-width) toggle key. (KEYCODE_ZENKAKU_HANKAKU)
] as const

export type FunctionKey = typeof functionKeys[number];
export function isFunctionKey(key: string): key is FunctionKey { return functionKeys.some(k => k === key); }
const functionKeyType = 'Function';
const functionKeys = [
  'F1', // The F1 key, a general purpose function key, as index 1.
  'F2', // The F2 key, a general purpose function key, as index 2.
  'F3', // The F3 key, a general purpose function key, as index 3.
  'F4', // The F4 key, a general purpose function key, as index 4.
  'F5', // The F5 key, a general purpose function key, as index 5.
  'F6', // The F6 key, a general purpose function key, as index 6.
  'F7', // The F7 key, a general purpose function key, as index 7.
  'F8', // The F8 key, a general purpose function key, as index 8.
  'F9', // The F9 key, a general purpose function key, as index 9.
  'F10', // The F10 key, a general purpose function key, as index 10.
  'F11', // The F11 key, a general purpose function key, as index 11.
  'F12', // The F12 key, a general purpose function key, as index 12.
  'Soft1', // General purpose virtual function key, as index 1.
  'Soft2', // General purpose virtual function key, as index 2.
  'Soft3', // General purpose virtual function key, as index 3.
  'Soft4', // General purpose virtual function key, as index 4.
] as const

export type MultimediaKey = typeof multimediaKeys[number];
export function isMultimediaKey(key: string): key is MultimediaKey { return multimediaKeys.some(k => k === key); }
const multimediaKeyType = 'Multimedia';
const multimediaKeys = [
  'ChannelDown', // Select next (numerically or logically) lower channel. (APPCOMMAND_MEDIA_CHANNEL_DOWN, KEYCODE_CHANNEL_DOWN)
  'ChannelUp', // Select next (numerically or logically) higher channel. (APPCOMMAND_MEDIA_CHANNEL_UP, KEYCODE_CHANNEL_UP)
  'Close', // Close the current document or message (Note: This doesn’t close the application). (APPCOMMAND_CLOSE)
  'MailForward', // Open an editor to forward the current message. (APPCOMMAND_FORWARD_MAIL)
  'MailReply', // Open an editor to reply to the current message. (APPCOMMAND_REPLY_TO_MAIL)
  'MailSend', // Send the current message. (APPCOMMAND_SEND_MAIL)
  'MediaClose', // Close the current media, for example to close a CD or DVD tray. (KEYCODE_MEDIA_CLOSE)
  'MediaFastForward', // Initiate or continue forward playback at faster than normal speed, or increase speed if already fast forwarding. (APPCOMMAND_MEDIA_FAST_FORWARD, KEYCODE_MEDIA_FAST_FORWARD)
  'MediaPause', // Pause the currently playing media. (APPCOMMAND_MEDIA_PAUSE, KEYCODE_MEDIA_PAUSE)Media controller devices should use this value rather than "Pause" for their pause keys.
  'MediaPlay', // Initiate or continue media playback at normal speed, if not currently playing at normal speed. (APPCOMMAND_MEDIA_PLAY, KEYCODE_MEDIA_PLAY)
  'MediaPlayPause', // Toggle media between play and pause states. (APPCOMMAND_MEDIA_PLAY_PAUSE, KEYCODE_MEDIA_PLAY_PAUSE)
  'MediaRecord', // Initiate or resume recording of currently selected media. (APPCOMMAND_MEDIA_RECORD, KEYCODE_MEDIA_RECORD)
  'MediaRewind', // Initiate or continue reverse playback at faster than normal speed, or increase speed if already rewinding. (APPCOMMAND_MEDIA_REWIND, KEYCODE_MEDIA_REWIND)
  'MediaStop', // Stop media playing, pausing, forwarding, rewinding, or recording, if not already stopped. (APPCOMMAND_MEDIA_STOP, KEYCODE_MEDIA_STOP)
  'MediaTrackNext', // Seek to next media or program track. (APPCOMMAND_MEDIA_NEXTTRACK, KEYCODE_MEDIA_NEXT)
  'MediaTrackPrevious', // Seek to previous media or program track. (APPCOMMAND_MEDIA_PREVIOUSTRACK, KEYCODE_MEDIA_PREVIOUS)
  'New', // Open a new document or message. (APPCOMMAND_NEW)
  'Open', // Open an existing document or message. (APPCOMMAND_OPEN)
  'Print', // Print the current document or message. (APPCOMMAND_PRINT)
  'Save', // Save the current document or message. (APPCOMMAND_SAVE)
  'SpellCheck', // Spellcheck the current document or selection. (APPCOMMAND_SPELL_CHECK)
] as const

export type MultimediaNumpadKey = typeof multimediaNumpadKeys[number];
export function isMultimediaNumpadKey(key: string): key is MultimediaNumpadKey { return multimediaNumpadKeys.some(k => k === key); }
const multimediaNumpadKeyType = 'MultimediaNumpadKey';
const multimediaNumpadKeys = [
  'Key11', // The 11 key found on media numpads that have buttons from 1 ... 12.
  'Key12', // The 12 key found on media numpads that have buttons from 1 ... 12.
] as const

export type AudioKey = typeof audioKeys[number];
export function isAudioKey(key: string): key is AudioKey { return audioKeys.some(k => k === key); }
const audioKeyType = 'Audio';
const audioKeys = [
  'AudioBalanceLeft', // Adjust audio balance leftward. (VK_AUDIO_BALANCE_LEFT)
  'AudioBalanceRight', // Adjust audio balance rightward. (VK_AUDIO_BALANCE_RIGHT)
  'AudioBassBoostDown', // Decrease audio bass boost or cycle down through bass boost states. (APPCOMMAND_BASS_DOWN, VK_BASS_BOOST_DOWN)
  'AudioBassBoostToggle', // Toggle bass boost on/off. (APPCOMMAND_BASS_BOOST)
  'AudioBassBoostUp', // Increase audio bass boost or cycle up through bass boost states. (APPCOMMAND_BASS_UP, VK_BASS_BOOST_UP)
  'AudioFaderFront', // Adjust audio fader towards front. (VK_FADER_FRONT)
  'AudioFaderRear', // Adjust audio fader towards rear. (VK_FADER_REAR)
  'AudioSurroundModeNext', // Advance surround audio mode to next available mode. (VK_SURROUND_MODE_NEXT)
  'AudioTrebleDown', // Decrease treble. (APPCOMMAND_TREBLE_DOWN)
  'AudioTrebleUp', // Increase treble. (APPCOMMAND_TREBLE_UP)
  'AudioVolumeDown', // Decrease audio volume. (APPCOMMAND_VOLUME_DOWN, KEYCODE_VOLUME_DOWN)
  'AudioVolumeUp', // Increase audio volume. (APPCOMMAND_VOLUME_UP, KEYCODE_VOLUME_UP)
  'AudioVolumeMute', // Toggle between muted state and prior volume level. (APPCOMMAND_VOLUME_MUTE, KEYCODE_VOLUME_MUTE)
  'MicrophoneToggle', // Toggle the microphone on/off. (APPCOMMAND_MIC_ON_OFF_TOGGLE)
  'MicrophoneVolumeDown', // Decrease microphone volume. (APPCOMMAND_MICROPHONE_VOLUME_DOWN)
  'MicrophoneVolumeUp', // Increase microphone volume. (APPCOMMAND_MICROPHONE_VOLUME_UP)
  'MicrophoneVolumeMute', // Mute the microphone. (APPCOMMAND_MICROPHONE_VOLUME_MUTE, KEYCODE_MUTE)
] as const

export type SpeechKey = typeof speechKeys[number];
export function isSpeechKey(key: string): key is SpeechKey { return speechKeys.some(k => k === key); }
const speechKeyType = 'Speech';
const speechKeys = [
  'SpeechCorrectionList', // Show correction list when a word is incorrectly identified. (APPCOMMAND_CORRECTION_LIST)
  'SpeechInputToggle', // Toggle between dictation mode and command/control mode. (APPCOMMAND_DICTATE_OR_COMMAND_CONTROL_TOGGLE)
] as const

export type ApplicationKey = typeof applicationKeys[number];
export function isApplicationKey(key: string): key is ApplicationKey { return applicationKeys.some(k => k === key); }
const applicationKeyType = 'Application';
const applicationKeys = [
  'LaunchApplication1', // The first generic "LaunchApplication" key. This is commonly associated with launching "My Computer", and may have a computer symbol on the key. (APPCOMMAND_LAUNCH_APP1)
  'LaunchApplication2', // The second generic "LaunchApplication" key. This is commonly associated with launching "Calculator", and may have a calculator symbol on the key. (APPCOMMAND_LAUNCH_APP2, KEYCODE_CALCULATOR)
  'LaunchCalendar', // The "Calendar" key. (KEYCODE_CALENDAR)
  'LaunchContacts', // The "Contacts" key. (KEYCODE_CONTACTS)
  'LaunchMail', // The "Mail" key. (APPCOMMAND_LAUNCH_MAIL)
  'LaunchMediaPlayer', // The "Media Player" key. (APPCOMMAND_LAUNCH_MEDIA_SELECT)
  'LaunchMusicPlayer', // The "Music Player" key.
  'LaunchPhone', // The "Phone" key.
  'LaunchScreenSaver', // The "Screen Saver" key.
  'LaunchSpreadsheet', // The "Spreadsheet" key.
  'LaunchWebBrowser', // The "Web Browser" key.
  'LaunchWebCam', // The "WebCam" key.
  'LaunchWordProcessor', // The "Word Processor" key.
] as const

export type BrowserKey = typeof browserKeys[number];
export function isBrowserKey(key: string): key is BrowserKey { return browserKeys.some(k => k === key); }
const browserKeyType = 'Browser';
const browserKeys = [
  'BrowserBack', // Navigate to previous content or page in current history. (APPCOMMAND_BROWSER_BACKWARD)
  'BrowserFavorites', // Open the list of browser favorites. (APPCOMMAND_BROWSER_FAVORITES)
  'BrowserForward', // Navigate to next content or page in current history. (APPCOMMAND_BROWSER_FORWARD)
  'BrowserHome', // Go to the user’s preferred home page. (APPCOMMAND_BROWSER_HOME)
  'BrowserRefresh', // Refresh the current page or content. (APPCOMMAND_BROWSER_REFRESH)
  'BrowserSearch', // Call up the user’s preferred search page. (APPCOMMAND_BROWSER_SEARCH)
  'BrowserStop', // Stop loading the current page or content. (APPCOMMAND_BROWSER_STOP)
] as const

export type MobilePhoneKey = typeof mobilePhoneKeys[number];
export function isMobilePhoneKey(key: string): key is MobilePhoneKey { return mobilePhoneKeys.some(k => k === key); }
const mobilePhoneKeyType = 'MobilePhone';
const mobilePhoneKeys = [
  'AppSwitch', // The Application switch key, which provides a list of recent apps to switch between. (KEYCODE_APP_SWITCH)
  'Call', // The Call key. (KEYCODE_CALL)
  'Camera', // The Camera key. (KEYCODE_CAMERA)
  'CameraFocus', // The Camera focus key. (KEYCODE_FOCUS)
  'EndCall', // The End Call key. (KEYCODE_ENDCALL)
  'GoBack', // The Back key. (KEYCODE_BACK)
  'GoHome', // The Home key, which goes to the phone’s main screen. (KEYCODE_HOME)
  'HeadsetHook', // The Headset Hook key. (KEYCODE_HEADSETHOOK)
  'LastNumberRedial', // The Last Number Redial key.
  'Notification', // The Notification key. (KEYCODE_NOTIFICATION)
  'MannerMode', // Toggle between manner mode state: silent, vibrate, ring, ... (KEYCODE_MANNER_MODE)
  'VoiceDial', // The Voice Dial key.
] as const

export type TVKey = typeof tvKeys[number];
export function isTVKey(key: string): key is TVKey { return tvKeys.some(k => k === key); }
const tvKeyType = 'TV';
const tvKeys = [
  'TV', // Switch to viewing TV. (KEYCODE_TV)
  'TV3DMode', // TV 3D Mode. (KEYCODE_3D_MODE)
  'TVAntennaCable', // Toggle between antenna and cable input. (KEYCODE_TV_ANTENNA_CABLE)
  'TVAudioDescription', // Audio description. (KEYCODE_TV_AUDIO_DESCRIPTION)
  'TVAudioDescriptionMixDown', // Audio description mixing volume down. (KEYCODE_TV_AUDIO_DESCRIPTION_MIX_DOWN)
  'TVAudioDescriptionMixUp', // Audio description mixing volume up. (KEYCODE_TV_AUDIO_DESCRIPTION_MIX_UP)
  'TVContentsMenu', // Contents menu. (KEYCODE_TV_CONTENTS_MENU)
  'TVDataService', // Contents menu. (KEYCODE_TV_DATA_SERVICE)
  'TVInput', // Switch the input mode on an external TV. (KEYCODE_TV_INPUT)
  'TVInputComponent1', // Switch to component input #1. (KEYCODE_TV_INPUT_COMPONENT_1)
  'TVInputComponent2', // Switch to component input #2. (KEYCODE_TV_INPUT_COMPONENT_2)
  'TVInputComposite1', // Switch to composite input #1. (KEYCODE_TV_INPUT_COMPOSITE_1)
  'TVInputComposite2', // Switch to composite input #2. (KEYCODE_TV_INPUT_COMPOSITE_2)
  'TVInputHDMI1', // Switch to HDMI input #1. (KEYCODE_TV_INPUT_HDMI_1)
  'TVInputHDMI2', // Switch to HDMI input #2. (KEYCODE_TV_INPUT_HDMI_2)
  'TVInputHDMI3', // Switch to HDMI input #3. (KEYCODE_TV_INPUT_HDMI_3)
  'TVInputHDMI4', // Switch to HDMI input #4. (KEYCODE_TV_INPUT_HDMI_4)
  'TVInputVGA1', // Switch to VGA input #1. (KEYCODE_TV_INPUT_VGA_1)
  'TVMediaContext', // Media context menu. (KEYCODE_TV_MEDIA_CONTEXT_MENU)
  'TVNetwork', // Toggle network. (KEYCODE_TV_NETWORK)
  'TVNumberEntry', // Number entry. (KEYCODE_TV_NUMBER_ENTRY)
  'TVPower', // Toggle the power on an external TV. (KEYCODE_TV_POWER)
  'TVRadioService', // Radio. (KEYCODE_TV_RADIO_SERVICE)
  'TVSatellite', // Satellite. (KEYCODE_TV_SATELLITE)
  'TVSatelliteBS', // Broadcast Satellite. (KEYCODE_TV_SATELLITE_BS)
  'TVSatelliteCS', // Communication Satellite. (KEYCODE_TV_SATELLITE_CS)
  'TVSatelliteToggle', // Toggle between available satellites. (KEYCODE_TV_SATELLITE_SERVICE)
  'TVTerrestrialAnalog', // Analog Terrestrial. (KEYCODE_TV_TERRESTRIAL_ANALOG)
  'TVTerrestrialDigital', // Digital Terrestrial. (KEYCODE_TV_TERRESTRIAL_DIGITAL)
  'TVTimer', // Timer programming. (KEYCODE_TV_TIMER_PROGRAMMING)
] as const

export type MediaControllerKey = typeof mediaControllerKeys[number];
export function isMediaControllerKey(key: string): key is MediaControllerKey { return mediaControllerKeys.some(k => k === key); }
const mediaControllerKeyType = 'MediaController';
const mediaControllerKeys = [
  'AVRInput', // Switch the input mode on an external AVR (audio/video receiver). (KEYCODE_AVR_INPUT)
  'AVRPower', // Toggle the power on an external AVR (audio/video receiver). (KEYCODE_AVR_POWER)
  'ColorF0Red', // General purpose color-coded media function key, as index 0 (red). (VK_COLORED_KEY_0, KEYCODE_PROG_RED)
  'ColorF1Green', // General purpose color-coded media function key, as index 1 (green). (VK_COLORED_KEY_1, KEYCODE_PROG_GREEN)
  'ColorF2Yellow', // General purpose color-coded media function key, as index 2 (yellow). (VK_COLORED_KEY_2, KEYCODE_PROG_YELLOW)
  'ColorF3Blue', // General purpose color-coded media function key, as index 3 (blue). (VK_COLORED_KEY_3, KEYCODE_PROG_BLUE)
  'ColorF4Grey', // General purpose color-coded media function key, as index 4 (grey). (VK_COLORED_KEY_4)
  'ColorF5Brown', // General purpose color-coded media function key, as index 5 (brown). (VK_COLORED_KEY_5)
  'ClosedCaptionToggle', // Toggle the display of Closed Captions. (VK_CC, KEYCODE_CAPTIONS)
  'Dimmer', // Adjust brightness of device, by toggling between or cycling through states. (VK_DIMMER)
  'DisplaySwap', // Swap video sources. (VK_DISPLAY_SWAP)
  'DVR', // Select Digital Video Rrecorder. (KEYCODE_DVR)
  'Exit', // Exit the current application. (VK_EXIT)
  'FavoriteClear0', // Clear program or content stored as favorite 0. (VK_CLEAR_FAVORITE_0)
  'FavoriteClear1', // Clear program or content stored as favorite 1. (VK_CLEAR_FAVORITE_1)
  'FavoriteClear2', // Clear program or content stored as favorite 2. (VK_CLEAR_FAVORITE_2)
  'FavoriteClear3', // Clear program or content stored as favorite 3. (VK_CLEAR_FAVORITE_3)
  'FavoriteRecall0', // Select (recall) program or content stored as favorite 0. (VK_RECALL_FAVORITE_0)
  'FavoriteRecall1', // Select (recall) program or content stored as favorite 1. (VK_RECALL_FAVORITE_1)
  'FavoriteRecall2', // Select (recall) program or content stored as favorite 2. (VK_RECALL_FAVORITE_2)
  'FavoriteRecall3', // Select (recall) program or content stored as favorite 3. (VK_RECALL_FAVORITE_3)
  'FavoriteStore0', // Store current program or content as favorite 0. (VK_STORE_FAVORITE_0)
  'FavoriteStore1', // Store current program or content as favorite 1. (VK_STORE_FAVORITE_1)
  'FavoriteStore2', // Store current program or content as favorite 2. (VK_STORE_FAVORITE_2)
  'FavoriteStore3', // Store current program or content as favorite 3. (VK_STORE_FAVORITE_3)
  'Guide', // Toggle display of program or content guide. (VK_GUIDE, KEYCODE_GUIDE)
  'GuideNextDay', // If guide is active and displayed, then display next day’s content. (VK_NEXT_DAY)
  'GuidePreviousDay', // If guide is active and displayed, then display previous day’s content. (VK_PREV_DAY)
  'Info', // Toggle display of information about currently selected context or media. (VK_INFO, KEYCODE_INFO)
  'InstantReplay', // Toggle instant replay. (VK_INSTANT_REPLAY)
  'Link', // Launch linked content, if available and appropriate. (VK_LINK)
  'ListProgram', // List the current program. (VK_LIST)
  'LiveContent', // Toggle display listing of currently available live content or programs. (VK_LIVE)
  'Lock', // Lock or unlock current content or program. (VK_LOCK)
  'MediaApps', // Show a list of media applications: audio/video players and image viewers. (VK_APPS)Do not confuse this key value with the Windows' VK_APPS / VK_CONTEXT_MENU key, which is encoded as "ContextMenu".
  'MediaAudioTrack', // Audio track key. (KEYCODE_MEDIA_AUDIO_TRACK)
  'MediaLast', // Select previously selected channel or media. (VK_LAST, KEYCODE_LAST_CHANNEL)
  'MediaSkipBackward', // Skip backward to next content or program. (KEYCODE_MEDIA_SKIP_BACKWARD)
  'MediaSkipForward', // Skip forward to next content or program. (VK_SKIP, KEYCODE_MEDIA_SKIP_FORWARD)
  'MediaStepBackward', // Step backward to next content or program. (KEYCODE_MEDIA_STEP_BACKWARD)
  'MediaStepForward', // Step forward to next content or program. (KEYCODE_MEDIA_STEP_FORWARD)
  'MediaTopMenu', // Media top menu. (KEYCODE_MEDIA_TOP_MENU)
  'NavigateIn', // Navigate in. (KEYCODE_NAVIGATE_IN)
  'NavigateNext', // Navigate to next key. (KEYCODE_NAVIGATE_NEXT)
  'NavigateOut', // Navigate out. (KEYCODE_NAVIGATE_OUT)
  'NavigatePrevious', // Navigate to previous key. (KEYCODE_NAVIGATE_PREVIOUS)
  'NextFavoriteChannel', // Cycle to next favorite channel (in favorites list). (VK_NEXT_FAVORITE_CHANNEL)
  'NextUserProfile', // Cycle to next user profile (if there are multiple user profiles). (VK_USER)
  'OnDemand', // Access on-demand content or programs. (VK_ON_DEMAND)
  'Pairing', // Pairing key to pair devices. (KEYCODE_PAIRING)
  'PinPDown', // Move picture-in-picture window down. (VK_PINP_DOWN)
  'PinPMove', // Move picture-in-picture window. (VK_PINP_MOVE)
  'PinPToggle', // Toggle display of picture-in-picture window. (VK_PINP_TOGGLE)
  'PinPUp', // Move picture-in-picture window up. (VK_PINP_UP)
  'PlaySpeedDown', // Decrease media playback speed. (VK_PLAY_SPEED_DOWN)
  'PlaySpeedReset', // Reset playback to normal speed. (VK_PLAY_SPEED_RESET)
  'PlaySpeedUp', // Increase media playback speed. (VK_PLAY_SPEED_UP)
  'RandomToggle', // Toggle random media or content shuffle mode. (VK_RANDOM_TOGGLE)
  'RcLowBattery', // Not a physical key, but this key code is sent when the remote control battery is low. (VK_RC_LOW_BATTERY)
  'RecordSpeedNext', // Toggle or cycle between media recording speeds. (VK_RECORD_SPEED_NEXT)
  'RfBypass', // Toggle RF (radio frequency) input bypass mode (pass RF input directly to the RF output). (VK_RF_BYPASS)
  'ScanChannelsToggle', // Toggle scan channels mode. (VK_SCAN_CHANNELS_TOGGLE)
  'ScreenModeNext', // Advance display screen mode to next available mode. (VK_SCREEN_MODE_NEXT)
  'Settings', // Toggle display of device settings screen. (VK_SETTINGS, KEYCODE_SETTINGS)
  'SplitScreenToggle', // Toggle split screen mode. (VK_SPLIT_SCREEN_TOGGLE)
  'STBInput', // Switch the input mode on an external STB (set top box). (KEYCODE_STB_INPUT)
  'STBPower', // Toggle the power on an external STB (set top box). (KEYCODE_STB_POWER)
  'Subtitle', // Toggle display of subtitles, if available. (VK_SUBTITLE)
  'Teletext', // Toggle display of teletext, if available (VK_TELETEXT, KEYCODE_TV_TELETEXT).
  'VideoModeNext', // Advance video mode to next available mode. (VK_VIDEO_MODE_NEXT)
  'Wink', // Cause device to identify itself in some manner, e.g., audibly or visibly. (VK_WINK)
  'ZoomToggle', // Toggle between full-screen and scaled content, or alter magnification level. (VK_ZOOM, KEYCODE_TV_ZOOM_MODE)
] as const