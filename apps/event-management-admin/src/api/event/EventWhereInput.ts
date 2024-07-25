import { StringFilter } from "../../util/StringFilter";
import { DateTimeNullableFilter } from "../../util/DateTimeNullableFilter";
import { CustomerWhereUniqueInput } from "../customer/CustomerWhereUniqueInput";

export type EventWhereInput = {
  id?: StringFilter;
  date?: DateTimeNullableFilter;
  customer?: CustomerWhereUniqueInput;
};
