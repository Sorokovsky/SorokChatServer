import { OmitType, PartialType } from "@nestjs/swagger";
import { User } from "src/database/entities/user.entity";
import type { UserKey } from "./key.type";

export const omitedUpdateKeys: UserKey[] = ['id', 'createdAt', 'updatedAt', 'channels', 'messages']; 
export class UpdateUserDto extends PartialType(OmitType(User, omitedUpdateKeys)) {};