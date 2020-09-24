// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace OpenSkyRestClient.Utils
{
    public static class DateTimeUtils
    {
        public static int GetUnixTimestamp(DateTime value)
        {
            return (int) (value.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }
    }
}
