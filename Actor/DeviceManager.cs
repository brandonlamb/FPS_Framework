/*
  The Device Manager returns a List of InputEvents received from
  a specific device. This abstracts the logic of collecting input
  from different devices from the actual handling of those inputs.
*/
using Godot;
using System;
using System.Collections.Generic;

public class DeviceManager {
  public enum Devices{MouseAndKeyboard};
  private Devices device;
  List<bool> buttonsDown; // Store button states to check for changes.
  
  public DeviceManager(Devices device){
    this.device = device;
    buttonsDown = new List<bool>();
    switch(device){
      case Devices.MouseAndKeyboard:
        int buttonCount = 70; // TODO: Make this number exact.
        for(int i = 0; i < buttonCount; i++){ buttonsDown.Add(false); }
      break;  
    }
  }
  
  /* Returns inputs using device-specific method. */
  public List<InputEvent> GetInputEvents(){
    switch(device){
      case Devices.MouseAndKeyboard: return KeyboardEvents(); break;
    }
    return new List<InputEvent>();  
  }
  
  /* Convenience method for button down events. */
  private InputEvent Down(InputEvent.Buttons button){
      return new InputEvent(button, InputEvent.Actions.Down);
  }
  
  /* Convenience method for button up events. */
  private InputEvent Up(InputEvent.Buttons button){
      return new InputEvent(button, InputEvent.Actions.Up);
  }
  
  /* Returns a list of InputEvents for a given key press */
  private List<InputEvent> KeyEvents(int key, int buttonIndex, InputEvent.Buttons button){
    List<InputEvent> ret = new List<InputEvent>();
    bool press = Input.IsKeyPressed(key);
    if(press && !buttonsDown[buttonIndex]){ 
      ret.Add(Down(button));
      buttonsDown[buttonIndex] = true;
    }
    else if(!press && buttonsDown[buttonIndex]){ 
      ret.Add(Up(button));
      buttonsDown[buttonIndex] = false;
    }
    return ret;
  }
  
  /* Returns input events from MouseAndKeyboard inputs. */
  private List<InputEvent> KeyboardEvents(){
    List<InputEvent> ret = new List<InputEvent>();
    // TODO: Replace all literals with appropriate constants when
    // their location is known.
    ret.AddRange(KeyEvents(87, 0, InputEvent.Buttons.W));
    ret.AddRange(KeyEvents(65, 1, InputEvent.Buttons.A));
    ret.AddRange(KeyEvents(83, 2, InputEvent.Buttons.S));
    ret.AddRange(KeyEvents(68, 3, InputEvent.Buttons.D));
    ret.AddRange(KeyEvents(16777221, 4, InputEvent.Buttons.Enter)); // GD.KEY_RETURN is broken
    ret.AddRange(KeyEvents(16777232, 5, InputEvent.Buttons.Up));
    ret.AddRange(KeyEvents(16777234, 6, InputEvent.Buttons.Down));
    ret.AddRange(KeyEvents(16777231, 7, InputEvent.Buttons.Left));
    ret.AddRange(KeyEvents(16777233, 8, InputEvent.Buttons.Right));
    return ret;
  }
  
}
