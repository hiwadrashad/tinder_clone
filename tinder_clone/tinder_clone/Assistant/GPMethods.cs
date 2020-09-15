using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Xamarin.Forms;

namespace tinder_clone.GPMethods
{
    class GPMethods
    {
        public byte[] imagetobytearray(Image image)
        {
        //imagetobytearray
        var binFormatter = new BinaryFormatter();
        var mstream = new MemoryStream();
        binFormatter.Serialize(mstream, image);
            return mstream.ToArray();
        }

        //bytearrayToImage

        public ImageSource byteArrayToImageSource(byte[] byteArrayIn)
        {
            var ms = new MemoryStream(byteArrayIn);
   
            return ImageSource.FromStream(() => ms);
        }
    }
}
