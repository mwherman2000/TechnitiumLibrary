/*
Technitium Library
Copyright (C) 2019  Shreyas Zare (shreyas@technitium.com)

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.

*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TechnitiumLibrary.IO;

namespace TechnitiumLibrary.Net.Dns.ResourceRecords
{
    public class DnsDIDATHNRecord : DnsResourceRecordData
    {
        #region variables

        string _didathnTag; // optional primary key
        string _didathnDID; // optional secondary key
        string _didathnType;
        string _didathnAuthenticationPublicKey; // "value" field
        string _didathnControllerDID;

        #endregion

        #region constructor

        public DnsDIDATHNRecord(string value)
        {
            _didathnTag = "";
            _didathnDID = "";
            _didathnType = "";
            _didathnAuthenticationPublicKey = value;
            _didathnControllerDID = "";
        }

        public DnsDIDATHNRecord(Stream s)
            : base(s)
        { }

        public DnsDIDATHNRecord(dynamic jsonResourceRecord)
        {
            _length = Convert.ToUInt16(jsonResourceRecord.data.Value.Length);

            string[] parts = (jsonResourceRecord.data.Value as string).Split(' ');

            _didathnTag = parts[0];
            _didathnDID = parts[1];
            _didathnType = parts[2];
            _didathnAuthenticationPublicKey = parts[3];
            _didathnControllerDID = parts[4];
        }

        public DnsDIDATHNRecord(string didathnTag, string didathnDID, string didathnType, string didathnAuthenticationPublicKey, string didathnControllerDID)
        {
            _didathnTag = didathnTag;
            _didathnDID = didathnDID;
            _didathnType = didathnType;
            _didathnAuthenticationPublicKey = didathnAuthenticationPublicKey;
            _didathnControllerDID = didathnControllerDID;
        }

        #endregion

        #region protected

        protected override void Parse(Stream s)
        {
            int len = s.ReadByte();
            if (len < 0)
                throw new EndOfStreamException();
            _didathnTag = "";
            if (len > 0) _didathnTag = Encoding.ASCII.GetString(s.ReadBytes(len));

            len = s.ReadByte();
            if (len < 0)
                throw new EndOfStreamException();
            _didathnDID = "";
            if (len > 0) _didathnDID = Encoding.ASCII.GetString(s.ReadBytes(len));

            len = s.ReadByte();
            if (len < 0)
                throw new EndOfStreamException();
            _didathnType = "";
            if (len > 0) _didathnType = Encoding.ASCII.GetString(s.ReadBytes(len));

            len = s.ReadByte();
            if (len < 0)
                throw new EndOfStreamException();
            _didathnAuthenticationPublicKey = "";
            if (len > 0) _didathnAuthenticationPublicKey = Encoding.ASCII.GetString(s.ReadBytes(len));

            len = s.ReadByte();
            if (len < 0)
                throw new EndOfStreamException();
            _didathnControllerDID = "";
            if (len > 0) _didathnControllerDID = Encoding.ASCII.GetString(s.ReadBytes(len));
        }

        protected override void WriteRecordData(Stream s, List<DnsDomainOffset> domainEntries)
        {
            s.WriteByte(Convert.ToByte(_didathnTag.Length));
            if (_didathnTag.Length > 0) s.Write(Encoding.ASCII.GetBytes(_didathnTag));
            s.WriteByte(Convert.ToByte(_didathnDID.Length));
            if (_didathnDID.Length > 0) s.Write(Encoding.ASCII.GetBytes(_didathnDID));
            s.WriteByte(Convert.ToByte(_didathnType.Length));
            if (_didathnType.Length > 0) s.Write(Encoding.ASCII.GetBytes(_didathnType));
            s.WriteByte(Convert.ToByte(_didathnAuthenticationPublicKey.Length));
            if (_didathnAuthenticationPublicKey.Length > 0) s.Write(Encoding.ASCII.GetBytes(_didathnAuthenticationPublicKey));
            s.WriteByte(Convert.ToByte(_didathnControllerDID.Length));
            if (_didathnControllerDID.Length > 0) s.Write(Encoding.ASCII.GetBytes(_didathnControllerDID));
        }

        #endregion

        #region public

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            DnsDIDATHNRecord other = obj as DnsDIDATHNRecord;
            if (other == null)
                return false;

            if (this._didathnTag.Length > 0)
            {
                if (this._didathnTag != other._didathnTag)
                    return false;
            }
            else if (this._didathnDID.Length > 0)
            {
                if (this._didathnDID != other._didathnDID)
                    return false;
            }
            else
            {
                if ((this._didathnType.Length > 0) && (this._didathnType != other._didathnType))
                    return false;
                if (this._didathnAuthenticationPublicKey != other._didathnAuthenticationPublicKey)
                    return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return _didathnAuthenticationPublicKey.GetHashCode();
        }

        public override string ToString()
        {
            return DnsDatagram.EncodeCharacterString(_didathnTag + ":" + _didathnDID + "=" + _didathnType + "," + _didathnAuthenticationPublicKey + ", " + _didathnControllerDID);
        }

        #endregion

        #region properties

        public string Tag
        { get { return _didathnTag; } }
        public string DID
        { get { return _didathnDID; } }
        public string Type
        { get { return _didathnType; } }
        public string AuthenticationPublicKey
        { get { return _didathnAuthenticationPublicKey; } }
        public string ControllerDID
        { get { return _didathnControllerDID; } }

        #endregion
    }
}
