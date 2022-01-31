# Game of life SVG Generator

This is a simple implementation of Conway's game of life which can be used to
generate SVG/HTML files of the results.

## Example

```bash
./game-of-life --seed example-imports/gosper-glider-gun.csv -s 2 --tick 75 --time 10 -c "#FFAEBC" -bg "#B4F8C8"
```
![Animated Gosper glider gun example svg](./game-of-life/example/example-gosper-glider-gun.svg)