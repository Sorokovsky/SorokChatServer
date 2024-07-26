import { BadRequestException } from "@nestjs/common";

export class NotFoundException<T> extends BadRequestException {
    constructor(entity: string, key: keyof T, value: any) {
        super(`${entity} з ${key as string} === ${value} не знайдено.`);
    };
};