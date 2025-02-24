# MyCinema
# MyCinema Documentation

## Overview and Introduction
MyCinema is a web application designed to manage a cinema's ticketing system efficiently. It provides users with features such as movie searching, ticket purchasing, and comprehensive ticket-selling analytics. Administrators have additional privileges like adding salons, managing screenings, and viewing advanced analytics.

### Key Features
- **Authentication and Authorization:** Secure login and role-based access control.
- **Ticket Selling:** Users can purchase tickets for available movie screenings.
- **Movie Searching:** Search movies using data from TheMovieDB API.
- **Ticket Selling Analytics:** Comprehensive analytics for administrators, including gross and net income reports, and top-performing movies.

## Prerequisites
To set up and run MyCinema, the following are required:
- **.NET SDK 8**
- **SQL Server**
- **Visual Studio or VS Code**
- **Accounts on TheMovieDB and Stripe** (for API tokens)

## Installation and Setup
1. **Clone the repository:**
```sh
https://github.com/david20033/MyCinema
```
2. **Restore dependencies:**
```sh
dotnet restore
```
3. **Set up the database:**
   - Create a database named `MyCinemaDB`.
   - Update the connection string in `appsettings.Development.json` if needed.
4. **Apply migrations:**
```sh
dotnet ef database update
```
5. **Optional: Seed Database**
   - Log in with admin credentials:
     - Username: `admin@example.com`
     - Password: `123456a`
   - Navigate to Admin Panel > Settings and click 'Seed'. This action populates the database with test data, including:
     - 5 test users
     - All genres and languages from TheMovieDB
     - First 20 'Now Playing' movies from TheMovieDB
     - 2 theater salons with different capacities and seat placements
     - Screenings scheduled from the day after seeding to 7 days later, each with random ticket orders.

## Navigation and Layout
MyCinema features a consistent navigation bar across all pages with the following options:
- **MyCinema Logo:** Redirects to the home page.
- **Screenings:** Main tab showing 'Now Showing' movies.
- **About Us:** Information about the cinema and the application.
- **Search Bar:** Allows users to search for movies using TheMovieDB API.
- **Account Section:**
  - If not logged in: Shows 'Account' with sub-options for Login and Register.
  - If logged in: Displays the user's username and initials, with sub-options for 'Manage Account', 'Settings', and 'Sign Out'.
  - **Admin Panel** (visible to administrators only): Contains options for managing salons, screenings, movies, and accessing analytics.

## Features and Functionality
### 1. Screenings (Now Showing Movies)
- Displayed on the root page with a title 'Now Showing'.
- Users can filter screenings by date (from today up to 7 days ahead).
- Each movie card displays: Poster, Title, Overview, Genres, Runtime, Age Rating, and available screening times.
- Clicking on a movie title redirects users to the **Movie Details Page**.

### 2. Movie Details Page
- Shows detailed information including: Poster, Title, Original Title, Release Date, Overview, Directors, Cast (Top 10), Status, Runtime, Budget, Revenue, Production Companies, Language, Vote Average, Vote Count, and Collection (if applicable).
- Displays available screenings with date and time filtering if the movie is in the app's database.
- If not in the database and the user is an admin, they can add the movie to the database.

### 3. Ticket Ordering
- **Select Ticket Page:** Users select ticket type (Regular or VIP), quantity (1 to 8), and see the total price.
- **Select Seats Page:** Users pick seats using an interactive salon schema.
- **Confirm Order Page:** Displays ticket details and selected seats with a 'Pay' button.
- Redirects to Stripe's test checkout for payment.

### 4. Movie Search
- Available from the navigation bar.
- Redirects to a search results page displaying movies from TheMovieDB in a table view.
- Users can click on a row to view the movie's details.

### 5. Admin Panel
- **View Salons:** List of all salons with details like capacity, rows, and VIP status.
- **Add Salon:** Interactive form with seat schema generation.
- **All Screenings:** Movie timelines for all salons.
- **Now Playing:** Displays movies from TheMovieDB. Admins can select and add them to the app's database.
- **Movies in DB:** Shows movies stored in the app's database.
- **Add Screening:** Form for adding new screenings with validation.
- **Analytics Overview:** Graphs for gross and net income, and top 3 profitable movies.
- **Payment Analytics:** Detailed report of payments.
- **Settings:** Manage cinema open/close hours, ticket prices, and seed database.

## Developer Guide
- **Tech Stack:** .NET 8, SQL Server, ASP.NET Identity, TheMovieDB API, Stripe API.
- **Architecture Overview:** Follows MVC architecture pattern.
- **Code Structure:** Organized into Controllers, Views, and Models.
- **API Integration:**
  - TheMovieDB: For movie data.
  - Stripe: For payment processing.

## Screenshots
**Home Page**
![image](https://github.com/user-attachments/assets/d62a9ad4-fe07-4b5d-8702-d530a3434b23)
**Select Ticket**
![image](https://github.com/user-attachments/assets/36ccae57-27c9-4e06-abc8-9ba1889c889f)
**Select Seats**
![image](https://github.com/user-attachments/assets/74c60fb8-bab3-4d70-9707-22e94bd2fc2f)
**Confirm Order**
![image](https://github.com/user-attachments/assets/f84a5631-9849-416f-95b8-91cc74f955de)
**Movie Details page**
![image](https://github.com/user-attachments/assets/a6034775-059d-4973-bf10-f80026eff4b6)
**Admin Panel**
![image](https://github.com/user-attachments/assets/689c080a-8e15-4287-81ad-6536579b2553)
**All Screenings page**
![image](https://github.com/user-attachments/assets/3bb1634d-2f4c-4c75-9425-62b21b037c2d)
**Add Theatre Salon Form**
![image](https://github.com/user-attachments/assets/284ba947-d85e-4551-bdf6-b13d3b12c241)

## FAQ and Troubleshooting
- **Issue:** Database connection error.
  - **Solution:** Verify the connection string in `appsettings.Development.json`.
- **Issue:** TheMovieDB API key error.
  - **Solution:** Check and update the API key in the configuration.
- **Issue:** Unable to log in.
  - **Solution:** Ensure the database is seeded or the account exists.

## Conclusion
MyCinema is a comprehensive cinema ticketing solution with advanced features for both users and administrators. It leverages modern technologies and integrates seamlessly with TheMovieDB and Stripe APIs. This documentation provides complete guidance on setup, usage, and development, ensuring a smooth experience for all stakeholders.

