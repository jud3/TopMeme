# Top Meme
[![N|Solid](https://www.upload.ee/image/7572772/TopMemeBot.png)])
High performance OpenCV C# Colorbot.

## Features
  - Aim assist rather than aimbot
  - High performance
  - Magic
  - Smart target selection

## Scanner
Uses all CPU cores and OpenCV's fast pixel access to find bottom left corners of all the visible healthbars. It then checks in which direction you're moving your mouse and picks the target that is closest to the direction that you're moving, this gives you high degree of control while trying to switch between targets, aimbot shouldn't "jump back to target that you're not trying to aim at". However if you're trying to aim away from a single available target then it sometimes tries to move back to it because my mouse movement direction calculation seems to be biased upwards for some reason.

## OpenCV
It uses OpenCV for fast pixel access and attempted to use it to properly detect outlines but i never finished that feature. The idea that i was floating was that instead of trying to find "red" colours i would instead find all the pixels that have high contrasts above and below and same colors in the sides, allowing me to find horizontal pixels on enemy outlines, the algorithm that "might" be there got quite close, it was able to find all "close to mid range outlines" but it still had slightly too much noise to use properly but nothing that couldn't had been eliminated with some extra filtering.
With OpenCV you can actually do amazing amount of per-pixel checks before you start hitting performance issues.

## Aim circle
It uses aim circle instead of aim box giving it a much natural feeling when running the aim assist on "always on" mode instead of "push to aim" mode. And when i mean mode i mean "removing or adding input check in the code", you want it, you implement it. if(Input.KeyDown(Input.Keys.LBUTTON))...

## Settings
To increase or decrease aim circle's size use Page Up or Page Down, you'll see the effects immediatly on the form.

To move the aim offset use arrow keys, this is useful if you want different settings when playing Zarya or what not, also this should make it so that it supports any resolution. Press HOME button to save your current offset.

## Aim Position
Most pixel aimbots are really god damn inaccurate because they aim multiple frames into the past, what this does is that it adds extra offset to aim position depending on it's direction from the crosshair and it also adds extra offset depending on wether you're holding down A or D so if you're moving left aim assist is expecting the enemy to be more to the right so it adjusts it's target making the aim assist feel way more accurate.

## Still bad
I'm quite confident that Blizzard has the ability to determine wether you're aimbotting or not solely from analysing your mouse movement because you do end up with extra mouse_events that are easily detectable.

## Solution? Fully undetectable aimbot?
When you play on consoles you know how they achieve that smooth "aim assist" feeling? Your sensitivity simply gets slower the closer your crosshair is to the enemy...so i suggest that you reverse engineer your mouses USB DPI packet and simply change your DPI depending on the distance of the closest enemy to your crosshair and boom, you should potentially have completely undetectable aim assist...assuming that you're the only person with your particular build of the aim assist ofcourse. 

### Go nuts and have fun




