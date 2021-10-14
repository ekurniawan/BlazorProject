using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Act.Components
{
    public partial class Confirm : ComponentBase
    {
        public bool ShowConfirmation { get; set; }
        [Parameter]
        public string ConfirmationTitle { get; set; } = "Konfirmasi Hapus Data";
        [Parameter]
        public string ConfirmationMessage { get; set; } = "Apakah anda yakin untuk delete data?";

        public void Show()
        {
            ShowConfirmation = true;
            StateHasChanged();
        }

        [Parameter]
        public EventCallback<bool> ConfirmationChanged { get; set; }

        protected async Task OnConfirmationChange(bool value)
        {
            ShowConfirmation = false;
            await ConfirmationChanged.InvokeAsync(value);
        }

    }
}
