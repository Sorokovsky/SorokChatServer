import { PickType } from "@nestjs/swagger";
import { Message } from "src/database/entities/message.entity";
import type { MessageKey } from "./key.type";

export const createMessageKeys: MessageKey[] = ['text'];
export class CreateMessageDto extends PickType(Message, createMessageKeys) { };