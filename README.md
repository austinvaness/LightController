# ProPresenter Lighting Controller

A DMX lighting controller for use with ProPresenter.

---

## Config File

The following is a description of the settings found in the config.yml file, feel free to copy any snippets into the config and remove the lines starting with a "#" if desired.

### General Settings

```yml
# Name of the MIDI device, or blank to pick the first one
midi_device: 
# Scene to load on startup
default_scene: Scene
# The default transition time in seconds for switching scenes
default_transition_time: 1
```

### Scene Settings

Scenes are used to store lighting configurations. 
```yml
# List of lighting scenes
scenes:
- name: Scene
  # The amount of time in seconds to use as a transition into this scene
  # If left blank, the lighting controller will use the default value instead
  transition_time: 
  # The midi note and channel that will activate the scene
  midi_note:
    channel: 1
    note: 0
  # The scene input list
  inputs:
```

#### Scene Inputs

Each scene has a list of inputs that tell the light fixtures what color to be. Each input type has at least the following structure:
```yml
  - !type
    # Comma seperated list of fixture ids assigned to this input
    # Order may matter depending on the input type
    fixture_ids: 1,2,4-5
    # Maximum intensity as a percent or "auto" to calculate the intensity from amount of black in the color
    intensity: 100%
```

`!color_input` allows you to specify a color in RGB format.
Use an [online color picker](https://www.google.com/search?q=color+picker) to get the values.
```yml
  - !color_input
    # RGB color with each value between 0 and 255
    rgb:
      red: 255
      green: 255
      blue: 255
    fixture_ids: 1,2,4
    intensity: 40%
```

`!propresenter_input` uses the media that is showing on screen to generate the colors. The lighting controller will only look up the media once when the scene is activated, so if you change the background without reactivating the scene it will not update the light color.
```yml
  - !propresenter_input
    # True if you want the lights to change color while the media is playing, otherwise false
    has_motion: false
    # Minimum desired intensity
    min_intensity: 60%
    # Saturation where 100% is full color and 0% is white
    saturation: 100%
    fixture_ids: 7-13,6,14-20
    intensity: 90%
```

`!rainbow_input` cycles through all colors at the specified rate.
```yml
  - !rainbow_input
    # Saturation where 100% is full color and 0% is white
    saturation: 70%
    # The length of the color cycle in seconds
    cycle_length: 3
    fixture_ids: 6-19
    intensity: 90%
```

`!debug_input` creates a color picker below the scene list.
```yml
  - !debug_input
    fixture_ids: 6-20
    intensity: 100%
```

### DMX Settings

```yml
# DMX settings
dmx:
  # The index of the DMX device to use, starting at 0
  dmx_device: 0
  # List of all light fixture profiles
  fixtures:
  # List of all light fixture addresses
  addresses:
```

#### Fixture Profiles

The fixture profile list is where you define how the lights are hooked up to the DMX system. Each profile has the following structure:
```yml
    # The name of the fixture
  - name: 
    # The total number of DMX channels this fixture uses
    dmx_length: 1,2,4-5
    # A list of color values that correspond to each fixture
    address_map:
```
The `address_map` setting supports the following color values:
- Red, green, blue, white, or amber
- An RGB hex code color
- A color temperature
- Intensity
- A simple number between 0 and 255
- A blank line to indicate 0

Some examples of complete light fixture profiles:
```yml
  - name: RGBW
    dmx_length: 4
    address_map:
    - red
    - green
    - blue
    - 2700k
```

```yml
  - name: Lightbar
    dmx_length: 13
    address_map:
    - red
    - green
    - blue
    - white
    - amber
    - 
    - intensity
    - 255
```

#### Fixture Addresses

The fixture addresses is where you define how the lights are actually hooked up to the DMX system. Each address has the following structure:
```yml
    # The name of the fixture
  - name: Fixture
    # The DMX start address of the first light fixture
    start_address: 1
    # The number of light fixtures at this address
    count: 1
```

### ProPresenter Settings

The following settings must be configured in order to use ProPresenter inputs:
```yml
# ProPresenter settings
pro:
  # IP and port from ProPresenter network settings in this form: http://ip-address:port/v1/
  api_url: 
  # Path to the media assets folder, usually located in the Documents folder
  media_assets_path: 
```
---

## Changing Scenes with ProPresenter

To use ProPresenter to change the scenes, you can use the "Communication" MIDI action. Simply add a "MIDI Note On" action that corresponds to the scene's midi note in the config.

All media that is ready to be used by the ProPresenter input can be seen in the center of the window. If any media is currently being generated, the progress bar will activate to show the progress. In order to avoid having to wait before the light actually change color, make sure all the media is in the list before use in a real production. 

To further reduce the delay before the lights change, you may use the "Intensity" field of the MIDI action to give each media background an id. Ensure that the Intensity is at least one and that each background has a different Intensity value.

---

## Troubleshooting

If the program crashes, information on what could be wrong will be in the log files. You can access the log files via the program under the "Application" menu, or in the `%localappdata%\LightController\Logs` folder. An error may also be visible in the Windows Event Viewer.

If you are having other issues with the program behaving unexpectedly, you may try saving over the config via the "Config" menu or checking the logs for warnings. 