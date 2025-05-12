Some quick info:
1. All txt files are added as assembly resources with usage of only two of them (MiastaWspolrzedne2, MiastaPołączenia;
    from the start I intented to do all tasks and MiastaWspolrzedne is just the same but without air quality).
2. I interpreted task number 5 as follows: find the trip with the best possible average air quality and additional requirements as 2000 km limit and 5 cities between.
   To do this I've written recurency function with backtracking to explore all possibilities with given arhuments where the overall road judgment is average air quality.
3. For task 6 I've created a model with two-way referencing to make 'a web' where every element(city) is connected with one or more elements(cities) with distance given for every connection.
   This way I could create iterative function with heap to explore every possible road to find the min distance sequence.
4. Some small mistakes in txt files: like Piotrków Tryb is ended with comma not with dot or lack of spacing in one line - I've fixed these.
