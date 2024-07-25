import { Event } from "../event/Event";

export type Customer = {
  id: string;
  createdAt: Date;
  updatedAt: Date;
  firstName: string | null;
  lastName: string | null;
  phn: string | null;
  events?: Array<Event>;
};
