using System.Collections;
using System.Collections.Generic;

/// <summary>
/// with this class you can store and access variables from anywhere in you project.
/// note this is not ThreadSave yet.
/// </summary>
public static class Blackboard {

    #region ===== ===== DATA ===== =====

    ///<summary>
    /// all Blackboards of all types that have already be initialized. note the ID is always a string
    /// </summary>
    static Dictionary<System.Type, Dictionary<string, List<object>>> _library = new Dictionary<System.Type, Dictionary<string, List<object>>>();

    #endregion
    #region ===== ===== CORE ===== =====

    /// <summary>
    /// !!USE API!!
    /// Creates an new Blackboard.
    /// </summary>
    /// <typeparam name="T">the Type of the Blackboard (int, string, Transform ...)</typeparam>
    /// <returns>false if this BlackboardType already existes</returns>
    public static bool NewBlackboard<T>() {
        if (_library.ContainsKey(typeof(T))) {
            return false;
        }

        _library.Add(typeof(T), new Dictionary<string, List<object>>());
        return true;
    }

    #endregion
    #region ===== ===== API ===== =====

    /// <summary>
    /// API;
    /// Adds a new Variable to the Blackboard.
    /// will overwrite the value if the id already exists.
    /// </summary>
    /// <typeparam name="T">the type of the variable (will be set automaticly throw the variable)</typeparam>
    /// <param name="id">the ID of the Variable</param>
    /// <param name="value">the variable to store</param>
    /// <param name="pos">the position, witch value to write to (if you want to save multible values to one id) dafault value = 0</param>
    public static void AddValueToBlackboard<T>(string id, T value, int pos = 0) {
        if (!_library.ContainsKey(typeof(T))) {
            NewBlackboard<T>();
        }

        if (!_library[typeof(T)].ContainsKey(id)) {
            _library[typeof(T)].Add(id, new List<object>());
        }
        if (_library[typeof(T)][id].Count <= pos) {
            //Library[typeof(T)][id].Capacity = pos + 1;
            for(int i = _library[typeof(T)][id].Count; i <= pos; i++) {
                _library[typeof(T)][id].Add(null);
            }
        }
        _library[typeof(T)][id][pos] = value;
    }

    /// <summary>
    /// API;
    /// Accesses a variable that has already been set.
    /// </summary>
    /// <typeparam name="T">the type of the variable (will be set automaticly throw the variable)</typeparam>
    /// <param name="id">the ID of the Variable</param>
    /// <param name="value">the variable the value will be stort at</param>
    /// <param name="pos">the position on whitch value to access (if you have multible values at one id) dafault value = 0</param>
    /// <returns>true if the value was found and has been set</returns>
    public static bool GetValueFromBlackboard<T>(out T value, string id, int pos = 0) {
        if (!_library.ContainsKey(typeof(T)) || !_library[typeof(T)].ContainsKey(id) || _library[typeof(T)][id].Count <= pos || _library[typeof(T)][id][pos] == null) {
            value = default(T);
            return false;
        }

        value = (T)_library[typeof(T)][id][pos];
        return true;
    }

    #endregion
}
