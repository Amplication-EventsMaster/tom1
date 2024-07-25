import { CustomerWhereUniqueInput } from "../customer/CustomerWhereUniqueInput";

export type EventUpdateInput = {
  date?: Date | null;
  customer?: CustomerWhereUniqueInput | null;
};
