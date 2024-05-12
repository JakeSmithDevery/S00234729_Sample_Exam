using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace S00234729_Sample_Exam
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Movie selectedMovie;
        private DateTime selectedDate;
        private int availableSeats;
        public MainWindow()
        {
            InitializeComponent();

            LoadMovies();
        }

        private void LoadMovies()
        {
            using (var context = new MovieData("JakeSmithDevery")) // Replace "YourFullName" with your actual full name
            {
                // Retrieve movies from the database
                var movies = context.Movies.ToList();

                // Bind movies to the ListBox
                lbx_MovieListings.ItemsSource = movies;
            }
        }

        private void lbx_MovieListings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedMovie = lbx_MovieListings.SelectedItem as Movie;

            if (selectedMovie != null)
            {
                // Update the TextBlock with the movie synopsis
                tblk_Synopsis.Text = selectedMovie.Description;
            }
            UpdateAvailableSeats();
        }

        private void Booking_DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedDate = Booking_DatePicker.SelectedDate ?? DateTime.Today;
            UpdateAvailableSeats();
        }

        private void UpdateAvailableSeats()
        {
            if (selectedMovie != null && selectedDate != null)
            {
                using (var context = new MovieData("YourFullName")) // Replace "YourFullName" with your actual full name
                {
                    // Truncate selectedDate to compare only the date part
                    var truncatedDate = selectedDate.Date;

                    // Query the database to determine available seats for the selected movie on the selected date
                    var bookingsCount = context.Bookings
                        .Count(b => b.MovieID == selectedMovie.MovieID && DbFunctions.TruncateTime(b.BookingDate) == truncatedDate);
                    availableSeats = 100 - bookingsCount;

                    // Update the display with the number of available seats
                    RemainingSeats.Text = availableSeats > 0 ? $"{availableSeats}" : "Sold out";
                }
            }
        }

        private void btn_Booking_Click(object sender, RoutedEventArgs e)
        {
            if (selectedMovie != null && selectedDate != null)
            {
                int requiredSeats;
                if (int.TryParse(RequiredSeats.Text, out requiredSeats))
                {
                    if (requiredSeats <= availableSeats)
                    {
                        using (var context = new MovieData("JakeSmithDevery")) // Replace "YourFullName" with your actual full name
                        {
                            // Create a new booking object
                            var newBooking = new Booking
                            {
                                MovieID = selectedMovie.MovieID,
                                BookingDate = selectedDate,
                                NumberOfTickets = requiredSeats
                            };

                            // Save the new booking to the database
                            context.Bookings.Add(newBooking);
                            context.SaveChanges();

                            // Update available seats
                            UpdateAvailableSeats();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Not enough available seats.");
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a valid number of seats.");
                }
            }
        }
    }
}
