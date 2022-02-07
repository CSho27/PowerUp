
export function textOutline(strokeWidth: string, strokeColor: string): string {
  return `
    --stroke-color:${strokeColor};
    --stroke-width:${strokeWidth};
    text-shadow: calc(var(--stroke-width) * 1) calc(var(--stroke-width) * 0) 0
      var(--stroke-color),
    calc(var(--stroke-width) * 0.9239) calc(var(--stroke-width) * 0.3827) 0
      var(--stroke-color),
    calc(var(--stroke-width) * 0.7071) calc(var(--stroke-width) * 0.7071) 0
      var(--stroke-color),
    calc(var(--stroke-width) * 0.3827) calc(var(--stroke-width) * 0.9239) 0
      var(--stroke-color),
    calc(var(--stroke-width) * 0) calc(var(--stroke-width) * 1) 0
      var(--stroke-color),
    calc(var(--stroke-width) * -0.3827) calc(var(--stroke-width) * 0.9239) 0
      var(--stroke-color),
    calc(var(--stroke-width) * -0.7071) calc(var(--stroke-width) * 0.7071) 0
      var(--stroke-color),
    calc(var(--stroke-width) * -0.9239) calc(var(--stroke-width) * 0.3827) 0
      var(--stroke-color),
    calc(var(--stroke-width) * -1) calc(var(--stroke-width) * 0) 0
      var(--stroke-color),
    calc(var(--stroke-width) * -0.9239) calc(var(--stroke-width) * -0.3827) 0
      var(--stroke-color),
    calc(var(--stroke-width) * -0.7071) calc(var(--stroke-width) * -0.7071) 0
      var(--stroke-color),
    calc(var(--stroke-width) * -0.3827) calc(var(--stroke-width) * -0.9239) 0
      var(--stroke-color),
    calc(var(--stroke-width) * 0) calc(var(--stroke-width) * -1) 0
      var(--stroke-color),
    calc(var(--stroke-width) * 0.3827) calc(var(--stroke-width) * -0.9239) 0
      var(--stroke-color),
    calc(var(--stroke-width) * 0.7071) calc(var(--stroke-width) * -0.7071) 0
      var(--stroke-color),
    calc(var(--stroke-width) * 0.9239) calc(var(--stroke-width) * -0.3827) 0
      var(--stroke-color);
  `
}