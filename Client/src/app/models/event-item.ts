import { Frequency } from "../enums/frequency";

export interface EventItem {
    id: string;
    name: string;
    description: string;
    frequency: Frequency;
    repeatTimeInFrequency: number;
    executionCount: number;
    status: boolean;
    createdDate: Date;
    modifiedDate: Date;
}