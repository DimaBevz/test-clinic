import { useState } from "react";

import ReactDatePicker from "react-datepicker";
import {
  FormControl,
  FormLabel,
  IconButton,
  Input,
  InputGroup,
  InputRightElement,
  forwardRef,
} from "@chakra-ui/react";
import { CalendarIcon } from "@chakra-ui/icons";
import { MdAccessTime } from "react-icons/md";

import "react-datepicker/dist/react-datepicker.css";
import { formatUTC } from "@utils/formatUTC";

import "./index.scss";

interface Props {
  htmlFor: string;
  dateFormat: string;
  onChange: (date: Date) => void;
  selectedDate: Date | undefined;
  label?: string;
  error?: string;
  disabled?: boolean;
  showTimeSelect?: boolean;
  showTimeSelectOnly?: boolean;
  timeIntervals?: number;
  timeCaption?: string;
}

const CustomInput = forwardRef(
  ({ value, onClick, showTimeSelectOnly, disabled }, ref) => (
    <InputGroup>
      <Input
        className="DatePicker__input"
        colorScheme="yellow"
        ref={ref}
        value={value}
        onClick={onClick}
        disabled={disabled}
        readOnly // Prevent manual editing
      />
      <InputRightElement>
        <IconButton
          disabled={disabled}
          colorScheme="yellow"
          aria-label="Select date"
          className={`${disabled ? "DatePicker__icon" : ""}`}
          icon={showTimeSelectOnly ? <MdAccessTime /> : <CalendarIcon />}
          onClick={disabled ? () => {} : onClick}
          size="sm"
        />
      </InputRightElement>
    </InputGroup>
  )
);

const DatePicker = ({
  htmlFor,
  label,
  dateFormat,
  disabled,
  selectedDate,
  onChange,
  showTimeSelect,
  showTimeSelectOnly,
  timeCaption,
  timeIntervals,
}: Props) => {
  const [isOpen, setIsOpen] = useState<boolean>(false);
  CustomInput.displayName = "CustomInput";
  
  return (
    <FormControl className="DatePicker">
      {label && (
        <FormLabel htmlFor={htmlFor} className="DatePicker__label">
          {label}
        </FormLabel>
      )}
      <ReactDatePicker
        dateFormat={dateFormat}
        selected={formatUTC(selectedDate, true)}
        popperPlacement="bottom-start"
        popperModifiers={[
          {
            name: "offset",
            options: {
              offset: [5, 10],
            },
          },
          {
            name: "preventOverflow",
            options: {
              rootBoundary: "viewport",
              tether: false,
              altAxis: true,
            },
          },
        ]}
        onChange={(date: Date) => {
          const value = formatUTC(date);
          onChange(value);
        }}
        customInput={
          <CustomInput
            showTimeSelectOnly={showTimeSelectOnly}
            disabled={disabled}
          />
        }
        open={isOpen}
        onClickOutside={() => setIsOpen(false)}
        onInputClick={() => setIsOpen(true)}
        disabled={disabled ?? false}
        showTimeSelect={showTimeSelect ?? false}
        showTimeSelectOnly={showTimeSelectOnly ?? false}
        timeCaption={timeCaption}
        timeIntervals={timeIntervals}
      />
    </FormControl>
  );
};

export default DatePicker;
