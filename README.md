# Library System - Borrow and Access Free Books

## Description

A web-based **Library System** that allows users to borrow books, with premium users getting access to free books. This system also features an integrated video section for educational content. The system includes a robust authentication mechanism, role-based access control (regular and premium users), and a simple user interface for easy interaction.

## Features

- **User Authentication:** Login and registration system with email/password authentication.
- **Borrowing Books:** Regular users can borrow books with a return deadline.
- **Premium Access:** Premium users can borrow books for free with no return deadline.
- **Search Books:** Search for books by title, author, or category.
- **Video Section:** Educational videos related to books and topics available for all users.
- **Admin Panel:** Admins can manage users, books, and video content.
- **Responsive UI:** User-friendly interface for both regular and premium users.

## Website Demo Video

Watch the demo of the Library System here:

[![Library System Demo]](https://youtu.be/zYRBzxtuB08)


## Technologies Used

- **Frontend:** React.js, HTML, CSS
- **Backend:** Node.js, Express.js
- **Database:** MongoDB (for storing user data, book inventory, etc.)
- **Authentication:** JWT (JSON Web Tokens) for secure login sessions
- **Video Hosting:** Embedded YouTube or custom video hosting solution
- **Payment Integration:** Stripe (for handling premium subscriptions)
- **Deployment:** Docker, VPS (or any cloud hosting)

## Setup & Installation

To set up the project locally, follow these steps:

### Prerequisites:
- Node.js (v14 or higher)
- MongoDB (or use a cloud service like MongoDB Atlas)
- Docker (optional for containerization)

### Installation:

1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/library-system.git
   cd library-system
