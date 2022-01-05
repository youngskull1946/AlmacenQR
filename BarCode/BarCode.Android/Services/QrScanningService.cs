using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BarCode.Services;
using Xamarin.Forms;
using ZXing.Mobile;

[assembly: Dependency(typeof(BarCode.Droid.Services.QrScanningService))]

namespace BarCode.Droid.Services
{
    class QrScanningService : ScanningService
    {
        public async Task<string> ScanAsync()
        {
            var optionDefault = new MobileBarcodeScanningOptions();
            var optionsCustom = new MobileBarcodeScanningOptions();

            var scanner = new MobileBarcodeScanner()
            {
                TopText = "Acerca la cámara",
                BottomText = "Toca la pantalla para enfocar",
            };
            var scanResult = await scanner.Scan(optionsCustom);
            return scanResult.Text;
        }
    }
}