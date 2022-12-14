import { Frequency } from "../enums/frequency";

export interface EventItem {
    id: string;
    name: string;
    description: string;
    frequency: Frequency;
    repeatTimeInFrequency: number;
    currentRepeatExecutionCount: number;
    lastControlDate: Date;
    lastRepeatDate: Date;
    nextRepeatDate: Date;
    pointValue: number;
    totalPoint: number;
    totalRepeatCount: number;
    totalExecutionCount: number;
    status: boolean;
    createdDate: Date;
    modifiedDate: Date;
}