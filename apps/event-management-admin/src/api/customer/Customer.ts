import { Event } from "../event/Event";

export type Customer = {
  createdAt: Date;
  events?: Array<Event>;
  firstName: string | null;
  id: string;
  lastName: string | null;
  phn: string | null;
  updatedAt: Date;
};
