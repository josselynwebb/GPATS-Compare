Public Class Pixci

    Public format As String = "default"
    Public formatfile As String

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

    'Example:
    'Dim PixciCamera As New Pixci()

End Class
