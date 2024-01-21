import { SyntheticEvent } from "react";
import { DateRangePicker as Datepicker, Stack } from "rsuite";
import { DateRange } from "rsuite/esm/DateRangePicker";

import "./index.scss";

interface DateRangePickerProps {
  value: [Date, Date];
  onChange: (
    value: DateRange | null,
    event: SyntheticEvent<Element, Event>
  ) => void;
  label?: string;
  dateFormat: string;
  defaultCalendarValue?: [Date, Date];
  disabled?: boolean;
  placeholder?: string;
  size: 'lg' | 'md' | 'sm' | 'xs'
}

export const DateRangePicker = ({
  value,
  onChange,
  label,
  dateFormat,
  defaultCalendarValue,
  disabled,
  placeholder,
  size
}: DateRangePickerProps) => {
  return (
    <Stack
      direction="column"
      spacing={8}
      alignItems="flex-start"
      className="DateRangePicker"
    >
      {label && <label className="DateRangePicker__label">{label}</label>}
      <Datepicker
        value={value}
        onChange={onChange}
        showMeridian
        format={dateFormat}
        defaultCalendarValue={defaultCalendarValue}
        disabled={disabled}
        placeholder={placeholder}
        size={size}
      />
    </Stack>
  );
};
