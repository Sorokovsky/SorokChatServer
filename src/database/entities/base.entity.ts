import { ApiProperty } from "@nestjs/swagger";
import { CreateDateColumn, PrimaryGeneratedColumn, UpdateDateColumn } from "typeorm";

export class BaseEntity {
    @ApiProperty({default: 1})
    @PrimaryGeneratedColumn()
    id: number;

    @ApiProperty({default: new Date()})
    @CreateDateColumn({name: "created_at"})
    createdAt: Date;

    @ApiProperty({default: new Date()})
    @UpdateDateColumn({name: "updated_at"})
    updatedAt: Date;
}