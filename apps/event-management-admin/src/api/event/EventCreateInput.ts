import { CustomerWhereUniqueInput } from "../customer/CustomerWhereUniqueInput";

export type EventCreateInput = {
  date?: Date | null;
  customer?: CustomerWhereUniqueInput | null;
};
