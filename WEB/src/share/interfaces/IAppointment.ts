interface TimeSlot {
  startTime: string;
  endTime: string;
}

interface SessionTemplate {
  dayLabel: string;
  timeSlots: TimeSlot[];
}

export interface SessionTemplatesObject {
    [dayLabel: string]: TimeSlot[];
  }

export type SessionTemplatesArray = SessionTemplate[];

