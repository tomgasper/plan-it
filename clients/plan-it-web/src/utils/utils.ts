export function getDifferenceInDays(d1: Date, d2: Date): number {
    const date1: Date = new Date(d1);
    const date2: Date = new Date(d2);
    
    // Calculate the difference in 
    // milliseconds between the two dates
    const differenceInMs: number = 
        Math.abs(date2.getTime() - date1.getTime());
    
    // Define the number of milliseconds in a day
    const millisecondsInDay: number = 1000 * 60 * 60 * 24;
    
    // Calculate the difference in days by 
    // dividing the difference in milliseconds by 
    // milliseconds in a day
    const differenceInDays: number = 
        Math.floor(differenceInMs / millisecondsInDay);
    
    // Output the result

        return differenceInDays;
}