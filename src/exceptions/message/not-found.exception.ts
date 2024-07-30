import { Message } from "src/database/entities/message.entity";
import { NotFoundException } from "../base/not-found.exception";

export class MessageNotFoundException extends NotFoundException<Message> {
    constructor(key: keyof Message, value: any) {
        super("Повідомлення", key, value);
    };
};