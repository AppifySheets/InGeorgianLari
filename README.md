# Georgian Lari Converter / ლარის კონვერტორი

A modern dark-themed currency converter for Georgian Lari (GEL) with real-time exchange rates from the National Bank of Georgia.

## Features

- 🇬🇪 **Georgian Language** - Full Georgian UI translation
- 🌙 **Dark Mode** - Modern dark theme design
- 📊 **Exchange Rate Chart** - 14-day historical rate visualization
- 💱 **Multiple Currencies** - Convert USD, EUR, and GBP to Georgian Lari
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

# Restore dependencies
dotnet restore

# Run the application
dotnet run

# Open http://localhost:5008 in your browser
```

### Building for Production
```bash
dotnet publish -c Release -o dist
```

## Deployment

### Automatic Deployment (Main Branch)
Every push to `main` automatically deploys to GitHub Pages via GitHub Actions.

### Manual Preview Deployment
You can deploy any branch for preview:

1. Go to [Actions](https://github.com/AppifySheets/InGeorgianLari/actions)
2. Select "Deploy Preview to GitHub Pages"
3. Click "Run workflow"
4. Select your branch and run

### GitHub Pages Setup
1. Go to Settings → Pages
2. Source: GitHub Actions
3. The site will be available at `https://[username].github.io/InGeorgianLari/`

## API Integration

The app uses the National Bank of Georgia's public API:
```
https://nbg.gov.ge/gw/api/ct/monetarypolicy/currencies/en/json
```

## Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## License

This project is open source and available under the MIT License.

*Collaboration by Claude*