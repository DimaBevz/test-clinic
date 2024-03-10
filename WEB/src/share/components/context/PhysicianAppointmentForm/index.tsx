import React, { Dispatch, ReactNode, SetStateAction, createContext } from "react";

// Define the type for your context
interface PhysicianAppointmentFormContextType {
  value: string;
  setValue: Dispatch<SetStateAction<string>>;
}

interface PhysicianAppointmentFormProviderProps {
    children: ReactNode;
  }

// Create a context with a default value (empty string and a dummy function)
export const PhysicianAppointmentFormContext =
  createContext<PhysicianAppointmentFormContextType>({
    value: "",
    setValue: () => {}, // Dummy function
  });

export const PhysicianAppointmentFormProvider = ({ children }: PhysicianAppointmentFormProviderProps) => {
  const [value, setValue] = React.useState<string>("");

  return (
    <PhysicianAppointmentFormContext.Provider
      value={{ value, setValue }}
    >
      {children}
    </PhysicianAppointmentFormContext.Provider>
  );
};
