import { Customer } from "../customer/Customer";

export type Event = {
  id: string;
  createdAt: Date;
  updatedAt: Date;
  date: Date | null;
  customer?: Customer | null;
};
