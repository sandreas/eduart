using CommunityToolkit.Mvvm.ComponentModel;
using eduart.Models;

namespace eduart.ViewModels.Game;

public partial class GameMainViewModel: ViewModelBase
{
    [ObservableProperty]
    private Profile _profile;
    
}