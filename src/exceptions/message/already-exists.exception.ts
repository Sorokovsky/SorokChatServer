import { Message } from 'src/database/entities/message.entity';
import { AlreadyExistsException } from '../base/already-exists.exception';
export class MessageAlreadyExistsException extends AlreadyExistsException<Message> {
    constructor(key: keyof Message, value: any) {
        super("Повідомлення", key, value);
    };
};