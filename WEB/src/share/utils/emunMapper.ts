export function enumNameMapper<E extends Record<string, number | string>>(
  enumObj: E
) {
  return (value: number): string => {
    const enumKey = Object.keys(enumObj).find((key) => enumObj[key] === value);
    return enumKey ? enumKey : "Unknown";
  };
}
