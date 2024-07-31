import { PickType } from "@nestjs/swagger";
import { User } from "src/database/entities/user.entity";
import { UserKey } from "../user/key.type";

export const registrationKeys: UserKey[] = ['email', 'password'];
export class RegistrationDto extends PickType(User, registrationKeys) { };