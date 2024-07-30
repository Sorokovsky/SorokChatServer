import { OmitType } from "@nestjs/swagger";
import { Message } from "src/database/entities/message.entity";
import { MessageKey } from "./key.type";

export const omitedGetMessageKeys: MessageKey[] = [];
export class GetMessageDto extends OmitType(Message, omitedGetMessageKeys) { };