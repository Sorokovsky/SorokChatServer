import { OmitType, PartialType } from "@nestjs/swagger";
import { Message } from "src/database/entities/message.entity";
import { MessageKey } from "./key.type";

export const omitedMessageUpdateKeys: MessageKey[] = ['author', 'channel', 'createdAt', 'updatedAt'];
export class UpdateMessageDto extends PartialType(OmitType(Message, omitedMessageUpdateKeys)) { };