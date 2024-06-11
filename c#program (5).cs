using System.Windows;

namespace BudgetManagementSystem
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnAnalyze_Click(object sender, RoutedEventArgs e)
        {
            decimal totalBudget;
            if (!decimal.TryParse(txtTotalBudget.Text, out totalBudget) || totalBudget <= 0)
            {
                txtAnalysis.Text = "Invalid budget amount. Please enter a valid positive number.";
                return;
            }

            decimal totalExpenses = 0;
            totalExpenses += GetExpense(txtFoodExpense, Category.Food);
            totalExpenses += GetExpense(txtClothesExpense, Category.Clothes);
            totalExpenses += GetExpense(txtFriendsExpense, Category.Friends);
            totalExpenses += GetExpense(txtEntertainmentExpense, Category.Entertainment);

            if (totalExpenses > totalBudget)
            {
                txtAnalysis.Text = "Your expenses exceed your budget. You may need to adjust your spending.";

                // Calculate the difference between expenses and budget
                decimal difference = totalExpenses - totalBudget;
                txtAnalysis.Text += $"\nYou are overspending by {difference}. Consider cutting down on non-essential expenses.";

                // Provide advice based on the main areas of overspending
                txtAnalysis.Text += "\nMain areas of overspending:";
                if (totalExpenses > totalBudget / 4) // If the expense exceeds 25% of the budget
                {
                    txtAnalysis.Text += "\n- You are spending a significant portion of your budget on ";
                    if (GetExpense(txtFoodExpense, Category.Food) > totalBudget / 4)
                        txtAnalysis.Text += "food. Consider finding cheaper alternatives or reducing the frequency of spending in this category.";
                    else if (GetExpense(txtClothesExpense, Category.Clothes) > totalBudget / 4)
                        txtAnalysis.Text += "clothes. Consider finding cheaper alternatives or reducing the frequency of spending in this category.";
                    else if (GetExpense(txtFriendsExpense, Category.Friends) > totalBudget / 4)
                        txtAnalysis.Text += "friends. Consider finding cheaper alternatives or reducing the frequency of spending in this category.";
                    else if (GetExpense(txtEntertainmentExpense, Category.Entertainment) > totalBudget / 4)
                        txtAnalysis.Text += "entertainment. Consider finding cheaper alternatives or reducing the frequency of spending in this category.";
                }
            }
            else
            {
                txtAnalysis.Text = "Your expenses are within your budget. Well done!";
            }
        }

        private decimal GetExpense(TextBox textBox, Category category)
        {
            decimal amount;
            if (!decimal.TryParse(textBox.Text, out amount) || amount < 0)
            {
                txtAnalysis.Text = "Invalid amount. Please enter a valid positive number.";
                return 0;
            }
            return amount;
        }
    }

    public enum Category
    {
        Food,
        Clothes,
        Friends,
        Entertainment
    }
}