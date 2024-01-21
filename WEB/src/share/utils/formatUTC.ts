export const formatUTC = (dateInt: any, addOffset = false) => {
  const date = !dateInt || dateInt.length < 1 ? new Date() : new Date(dateInt);
  if (typeof dateInt === "string") {
    return date;
  } else {
    const offset = addOffset
      ? date.getTimezoneOffset()
      : -date.getTimezoneOffset();
    const offsetDate = new Date();
    offsetDate.setTime(date.getTime() + offset * 60000);
    return offsetDate;
  }
};

export function convertTo12HourFormatUTC(isoString: string): string {
    const dateUTC: Date = new Date(isoString);
    let hours: number = dateUTC.getUTCHours();
    const minutes: number = dateUTC.getUTCMinutes();
    const ampm: string = hours >= 12 ? 'PM' : 'AM';
    hours = hours % 12;
    hours = hours ? hours : 12; // the hour '0' should be '12'
    const minutesStr: string = minutes < 10 ? '0' + minutes : minutes.toString();
    const strTime: string = hours + ':' + minutesStr + ' ' + ampm;
    return strTime;
}
