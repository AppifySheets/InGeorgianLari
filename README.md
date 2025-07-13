# Georgian Lari Converter / ლარის კონვერტორი

A modern dark-themed currency converter for Georgian Lari (GEL) with real-time exchange rates from the National Bank of Georgia.

## Features

- 🇬🇪 **Georgian Language** - Full Georgian UI translation
- 🌙 **Dark Mode** - Modern dark theme design
- 📊 **Exchange Rate Charts** - Historical rate visualization
- 💱 **Multiple Currencies** - Convert USD, EUR, and GBP to Georgian Lari
- 📈 **Historical Rates** - View exchange rate history over time
- 💰 **Purchasing Power** - Compare currency values over time
- 📱 **Responsive Design** - Works perfectly on all devices
- ⚡ **Real-time Rates** - Live data from National Bank of Georgia API
- 🚀 **GitHub Pages** - Automated deployment via GitHub Actions

## Live Demo

Visit: [https://appifysheets.github.io/InGeorgianLari/](https://appifysheets.github.io/InGeorgianLari/)

## Technology Stack

- **.NET 9** - Blazor WebAssembly
- **Chart.js** - Interactive rate history charts
- **National Bank of Georgia API** - Real-time exchange rates
- **GitHub Actions** - CI/CD pipeline
- **GitHub Pages** - Static site hosting

## Development

### Prerequisites
- .NET 9 SDK
- Visual Studio 2022 or VS Code

### Running Locally
```bash
# Clone the repository
git clone https://github.com/AppifySheets/InGeorgianLari.git
cd InGeorgianLari

# Build and run
./build-and-run.sh

# Or manually
dotnet build
dotnet run
```

The application will be available at `https://localhost:5001`

### Building for Production
```bash
dotnet publish -c Release -o publish
```

## API Integration

This application uses the National Bank of Georgia's official API for real-time exchange rates:
- API Endpoint: `https://nbg.gov.ge/api/GetCurrentRates`
- Updates: Daily at 11:00 AM Georgian time
- Currencies supported: USD, EUR, GBP, and more

## Deployment

The application automatically deploys to GitHub Pages when changes are pushed to the main branch:

1. GitHub Actions workflow builds the project
2. Publishes to `gh-pages` branch
3. Available at the GitHub Pages URL

## Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## License

This project is open source and available under the MIT License.

*Collaboration by Claude*