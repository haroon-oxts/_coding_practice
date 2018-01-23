using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

namespace MatrixRotateExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //float imu_to_vehicle_heading   = (float) WarmUpPlugin.CurrentDevice.DeviceConfig.DeviceToVehicleRotation.Imu2VehHeading.Value;
            //float imu_to_vehicle_pitch     = (float) WarmUpPlugin.CurrentDevice.DeviceConfig.DeviceToVehicleRotation.Imu2VehPitch.Value;
            //float imu_to_vehicle_roll      = (float) WarmUpPlugin.CurrentDevice.DeviceConfig.DeviceToVehicleRotation.InitialImu2VehRoll.Value;

            //float lever_arm_x = (float) WarmUpPlugin.CurrentDevice.DeviceConfig.PrimaryGNSSAntennaLeverArm.GAPx.Value;
            //float lever_arm_y = (float) WarmUpPlugin.CurrentDevice.DeviceConfig.PrimaryGNSSAntennaLeverArm.GAPy.Value;
            //float lever_arm_z = (float) WarmUpPlugin.CurrentDevice.DeviceConfig.PrimaryGNSSAntennaLeverArm.GAPz.Value;

            float imu_to_vehicle_heading   =   90f;
            float imu_to_vehicle_pitch     =    0f;
            float imu_to_vehicle_roll      =    0f;

            float lever_arm_x =   1;
            float lever_arm_y =  -1;
            float lever_arm_z =  -1;

            Vector3 lever_arm = new Vector3(lever_arm_x, lever_arm_y, lever_arm_z);

            //Account for the difference in the OxTS HPR and dot NET Numerics
            Matrix4x4 rotation = Matrix4x4.CreateFromYawPitchRoll(
                                    (imu_to_vehicle_pitch   * (float)(Math.PI / 180)),
                                    (imu_to_vehicle_roll    * (float)(Math.PI / 180)),
                                    (imu_to_vehicle_heading * (float)(Math.PI / 180))
                                    );
             
            Vector3 result_from_rotation   = Vector3.Transform(lever_arm, rotation);

            //VehicleFramePrimaryLevelArmX = result_from_rotation.X;
            //VehicleFramePrimaryLevelArmY = result_from_rotation.Y;
            //VehicleFramePrimaryLevelArmZ = result_from_rotation.Z;

            Oxts

        }
    }
}
