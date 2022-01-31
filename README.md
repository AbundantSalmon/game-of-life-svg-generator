# Game of life SVG Generator

This is a simple implementation of Conway's game of life which can be used to
generate SVG/HTML files of the results.

## Example

> ```./game-of-life --seed example-imports/gosper-glider-gun.csv -s 2 --tick 75 --time 10 -c "#FFAEBC" -bg "#B4F8C8"```

![Animated Gosper glider gun example
svg](./game-of-life/example/example-gosper-glider-gun.svg)

## Usage

```
Description:
  Generates Conway's game of life

Usage:
  game-of-life [options]

Options:
  --rows <rows>                   Number of rows [default: 10]
  --cols <cols>                   Number of columns [default: 10]
  --time <time>                   Time in seconds [default: 5]
  --tick <tick>                   Time between ticks in milliseconds [default: 500]
  -s, --spacing <spacing>         Spacing in pixels between the cells [default: 10]
  -r, --radius <radius>           Radius in pixels between the cells [default: 4]
  -c, --colour <colour>           Colour of the cells [default: black]
  -w, --width <width>             Width in pixels [default: 600]
  -h, --height <height>           Height in pixels [default: 600]
  -f, --filename <filename>       Filename without the extension [default: thing]
  --seed <seed>                   Seed file, should be a csv with with 1 in alive cells []
  -l, --linear                    Set the svg animation calcMode to linear, default is discrete [default: False]
  -bg, --background <background>  Background colour [default: rgba(0, 0, 0, 0)]
  --version                       Show version information
  -?, -h, --help                  Show help and usage information
```