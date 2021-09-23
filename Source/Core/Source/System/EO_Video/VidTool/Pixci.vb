''' <summary>
''' Example: Dim oPixciCam = New Pixci()
''' </summary>
''' <remarks></remarks>
Public Class Pixci

    'Default to Constant
    Public format As String = PIXCI_FORMAT

    Public formatfile As String

    Public BytesPerPixel As Double 'Added by Larry, was not in EPIX Sample Code
    Public maxWidth As Double
    Public maxHeight As Double
    Public minGain As Double
    Public maxGain As Double
    Public minGainB As Double
    Public maxGainB As Double
    Public minClock As Double
    Public maxClock As Double
    Public minOffset As Double
    Public maxOffset As Double
    Public minExposure As Double
    Public maxExposure As Double
    Public loFPS As Double
    Public hiFPS As Double

    Public is_live As Boolean = False
    Public do_SoftwareAxC As Boolean = False
    Public is_color As Boolean = False
End Class
