import { EventCreateNestedManyWithoutCustomersInput } from "./EventCreateNestedManyWithoutCustomersInput";

export type CustomerCreateInput = {
  firstName?: string | null;
  lastName?: string | null;
  phn?: string | null;
  events?: EventCreateNestedManyWithoutCustomersInput;
};
